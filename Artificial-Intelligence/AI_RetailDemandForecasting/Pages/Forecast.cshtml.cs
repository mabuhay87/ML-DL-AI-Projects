using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RetailDemandForecasting.Services;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace RetailDemandForecasting.Pages;

public class ForecastModel : PageModel
{
    private readonly CsvRetailDataLoader _loader;
    private readonly DemandForecastService _svc;
    private readonly IWebHostEnvironment _env;

    public ForecastModel(CsvRetailDataLoader loader, DemandForecastService svc, IWebHostEnvironment env)
    {
        _loader = loader;
        _svc = svc;
        _env = env;
    }

    [BindProperty] public bool BatchMode { get; set; } = false;

    [BindProperty] public int Store { get; set; } = 1;
    [BindProperty] public int Item { get; set; } = 1;
    [BindProperty] public int HorizonDays { get; set; } = 30;
    [BindProperty] public int HistoryPlotDays { get; set; } = 120;

    public ForecastResult? Result { get; set; }
    public string? Error { get; set; }
    public string? DataWarning { get; set; }

    public List<SelectListItem> StoreOptions { get; set; } = new();
    public string? StoreItemMapJson { get; set; }

    public string? ChartJson { get; set; }

    public List<BatchForecastRow>? BatchResults { get; set; }
    public string? BatchExportUrl { get; set; }

    public void OnGet()
    {
        LoadDropdownData();
    }

    public void OnPost()
    {
        try
        {
            LoadDropdownData();

            var trainPath = Path.Combine(_env.ContentRootPath, "Data", "train.csv");
            if (!System.IO.File.Exists(trainPath))
                throw new FileNotFoundException("Missing Data/train.csv. Add the CSV and set 'Copy to Output Directory = Copy if newer'.");

            var rows = _loader.LoadTrain(trainPath);

            if (BatchMode)
            {
                RunBatch(rows);
                return;
            }

            Result = _svc.ForecastForStoreItem(rows, Store, Item, HorizonDays, HistoryPlotDays);

            var labels = Result.History.Select(h => h.Date.ToString("yyyy-MM-dd"))
                .Concat(Result.Points.Select(p => p.Date.ToString("yyyy-MM-dd")))
                .ToArray();

            var history = Result.History.Select(h => (double)h.Sales).ToArray();
            var forecast = Result.Points.Select(p => (double)p.Forecast).ToArray();
            var lower = Result.Points.Select(p => (double)p.Lower).ToArray();
            var upper = Result.Points.Select(p => (double)p.Upper).ToArray();

            ChartJson = JsonSerializer.Serialize(new { labels, history, forecast, lower, upper });
        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
    }

    private void RunBatch(List<RetailDemandForecasting.Models.RetailSaleRow> rows)
    {
        var combos = rows
            .GroupBy(r => (r.store, r.item))
            .Select(g => new { g.Key.store, g.Key.item, Count = g.Count() })
            .Where(x => x.Count >= 90)
            .OrderBy(x => x.store).ThenBy(x => x.item)
            .ToList();

        var batch = new List<BatchForecastRow>();
        var errors = new List<string>();

        foreach (var c in combos)
        {
            try
            {
                var res = _svc.ForecastForStoreItem(rows, c.store, c.item, HorizonDays, historyToPlotDays: 0);

                var forecasts = res.Points.Select(p => p.Forecast).ToArray();
                var row = new BatchForecastRow
                {
                    Store = c.store,
                    Item = c.item,
                    HistoryCount = res.HistoryCount,
                    NextDayForecast = forecasts.Length > 0 ? forecasts[0] : 0,
                    AvgForecast = forecasts.Length > 0 ? forecasts.Average() : 0,
                    MinForecast = forecasts.Length > 0 ? forecasts.Min() : 0,
                    MaxForecast = forecasts.Length > 0 ? forecasts.Max() : 0
                };
                batch.Add(row);
            }
            catch (Exception ex)
            {
                errors.Add($"Store {c.store}, Item {c.item}: {ex.Message}");
            }
        }

        BatchResults = batch
            .OrderByDescending(b => b.AvgForecast)
            .ToList();

        var exportsDir = Path.Combine(_env.WebRootPath, "exports");
        Directory.CreateDirectory(exportsDir);

        var fileName = $"batch_forecast_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
        var exportPath = Path.Combine(exportsDir, fileName);

        var sb = new StringBuilder();
        sb.AppendLine("store,item,history_count,next_day_forecast,avg_forecast,min_forecast,max_forecast");
        foreach (var r in BatchResults)
        {
            sb.AppendLine(string.Join(",", new[]
            {
                r.Store.ToString(CultureInfo.InvariantCulture),
                r.Item.ToString(CultureInfo.InvariantCulture),
                r.HistoryCount.ToString(CultureInfo.InvariantCulture),
                r.NextDayForecast.ToString(CultureInfo.InvariantCulture),
                r.AvgForecast.ToString(CultureInfo.InvariantCulture),
                r.MinForecast.ToString(CultureInfo.InvariantCulture),
                r.MaxForecast.ToString(CultureInfo.InvariantCulture),
            }));
        }

        if (errors.Count > 0)
        {
            sb.AppendLine();
            sb.AppendLine("# Errors (some combos skipped):");
            foreach (var e in errors.Take(200))
                sb.AppendLine("# " + e);
        }

        System.IO.File.WriteAllText(exportPath, sb.ToString(), Encoding.UTF8);
        BatchExportUrl = "/exports/" + fileName;

        if (BatchResults.Count == 0)
            Error = "Batch completed, but no Store/Item combinations had enough history (>= 90 days).";
    }

    private void LoadDropdownData()
    {
        var trainPath = Path.Combine(_env.ContentRootPath, "Data", "train.csv");
        if (!System.IO.File.Exists(trainPath))
        {
            DataWarning = "Missing Data/train.csv. Add the CSV to /Data and set 'Copy to Output Directory = Copy if newer'. Dropdowns will appear once the file is available.";
            StoreOptions = new();
            StoreItemMapJson = JsonSerializer.Serialize(new Dictionary<string, List<int>>());
            return;
        }

        var rows = _loader.LoadTrain(trainPath);
        if (rows.Count == 0)
        {
            DataWarning = "train.csv was found but appears to be empty.";
            StoreOptions = new();
            StoreItemMapJson = JsonSerializer.Serialize(new Dictionary<string, List<int>>());
            return;
        }

        var stores = rows.Select(r => r.store).Distinct().OrderBy(s => s).ToList();

        var map = rows
            .GroupBy(r => r.store)
            .ToDictionary(
                g => g.Key.ToString(),
                g => g.Select(x => x.item).Distinct().OrderBy(i => i).ToList()
            );

        if (!stores.Contains(Store))
            Store = stores.First();

        if (map.TryGetValue(Store.ToString(), out var itemsForStore) && itemsForStore.Count > 0)
        {
            if (!itemsForStore.Contains(Item))
                Item = itemsForStore.First();
        }

        StoreOptions = stores
            .Select(s => new SelectListItem(s.ToString(), s.ToString(), s == Store))
            .ToList();

        StoreItemMapJson = JsonSerializer.Serialize(map);
    }
}
