using Microsoft.ML;
using Microsoft.ML.TimeSeries;
using RetailDemandForecasting.Models;

namespace RetailDemandForecasting.Services;

public class DemandForecastService
{
    private readonly MLContext _ml = new(seed: 1);

    private class SalesData { public float Value { get; set; } }

    private class SalesForecast
    {
        public float[] ForecastedSales { get; set; } = Array.Empty<float>();
        public float[] LowerBoundSales { get; set; } = Array.Empty<float>();
        public float[] UpperBoundSales { get; set; } = Array.Empty<float>();
    }

    public ForecastResult ForecastForStoreItem(
        List<RetailSaleRow> allRows,
        int store,
        int item,
        int horizonDays = 30,
        int historyToPlotDays = 120,
        int minHistoryRequired = 90)
    {
        var filtered = allRows
            .Where(r => r.store == store && r.item == item)
            .OrderBy(r => r.date)
            .ToList();

        if (filtered.Count < minHistoryRequired)
            throw new InvalidOperationException($"Not enough history for Store {store}, Item {item}. Need at least {minHistoryRequired} days.");

        var series = filtered.Select(r => new SalesData { Value = r.sales }).ToList();
        var dataView = _ml.Data.LoadFromEnumerable(series);

        int windowSize = 14;
        int seriesLength = Math.Min(90, series.Count);
        int trainSize = series.Count;

        var pipeline = _ml.Forecasting.ForecastBySsa(
            outputColumnName: nameof(SalesForecast.ForecastedSales),
            inputColumnName: nameof(SalesData.Value),
            windowSize: windowSize,
            seriesLength: seriesLength,
            trainSize: trainSize,
            horizon: horizonDays,
            confidenceLevel: 0.95f,
            confidenceLowerBoundColumn: nameof(SalesForecast.LowerBoundSales),
            confidenceUpperBoundColumn: nameof(SalesForecast.UpperBoundSales));

        var model = pipeline.Fit(dataView);

// Some ML.NET versions don't expose CreateTimeSeriesEngine. Use Transform + read the forecast vectors.
var transformed = model.Transform(dataView);
var forecast = _ml.Data.CreateEnumerable<SalesForecast>(transformed, reuseRowObject: false).First();
var lastDate = filtered.Max(r => r.date);

        var result = new ForecastResult
        {
            Store = store,
            Item = item,
            HorizonDays = horizonDays,
            HistoryCount = filtered.Count
        };

        if (historyToPlotDays > 0)
        {
            result.History = filtered
                .TakeLast(Math.Min(historyToPlotDays, filtered.Count))
                .Select(r => new HistoryPoint { Date = r.date, Sales = r.sales })
                .ToList();
        }

        for (int i = 0; i < horizonDays; i++)
        {
            result.Points.Add(new ForecastPoint
            {
                Date = lastDate.AddDays(i + 1),
                Forecast = forecast.ForecastedSales[i],
                Lower = forecast.LowerBoundSales[i],
                Upper = forecast.UpperBoundSales[i]
            });
        }

        return result;
    }
}
