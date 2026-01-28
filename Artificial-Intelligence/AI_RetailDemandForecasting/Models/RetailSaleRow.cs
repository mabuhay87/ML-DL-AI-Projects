namespace RetailDemandForecasting.Models;

public class RetailSaleRow
{
    public DateTime date { get; set; }
    public int store { get; set; }
    public int item { get; set; }
    public float sales { get; set; }
}
