using CsvHelper;
using CsvHelper.Configuration;
using RetailDemandForecasting.Models;
using System.Globalization;

namespace RetailDemandForecasting.Services;

public class CsvRetailDataLoader
{
    public List<RetailSaleRow> LoadTrain(string path)
    {
        using var reader = new StreamReader(path);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        };

        using var csv = new CsvReader(reader, config);
        return csv.GetRecords<RetailSaleRow>().ToList();
    }
}
