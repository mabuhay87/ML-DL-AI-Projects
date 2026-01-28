namespace RetailDemandForecasting.Services;

public class HistoryPoint
{
    public DateTime Date { get; set; }
    public float Sales { get; set; }
}

public class ForecastPoint
{
    public DateTime Date { get; set; }
    public float Forecast { get; set; }
    public float Lower { get; set; }
    public float Upper { get; set; }
}

public class ForecastResult
{
    public int Store { get; set; }
    public int Item { get; set; }
    public int HorizonDays { get; set; }
    public int HistoryCount { get; set; }
    public List<HistoryPoint> History { get; set; } = new();
    public List<ForecastPoint> Points { get; set; } = new();
}

public class BatchForecastRow
{
    public int Store { get; set; }
    public int Item { get; set; }
    public int HistoryCount { get; set; }
    public float NextDayForecast { get; set; }
    public float AvgForecast { get; set; }
    public float MinForecast { get; set; }
    public float MaxForecast { get; set; }
}
