namespace SalesDataAnalyzer;

public class SalesData
{
    public string Region {get; set;}
    public string Country {get; set;}
    public string ItemType {get; set;}
    public string SalesChannel {get; set;}
    public string OrderPriority {get; set;}
    public DateTime OrderDate {get; set;}
    public long OrderID {get; set;}
    public DateTime ShipDate {get; set;}
    public int UnitsSold {get; set;}
    public double UnitPrice {get; set;}
    public double UnitCost {get; set;}

    public SalesData(string region, string country, string itemType, string salesChannel, string orderPriority, DateTime orderDate, long orderID, DateTime shipDate, int unitsSold, double unitPrice, double unitCost)
    {
        Region = region;
        Country = country;
        ItemType = itemType;
        SalesChannel = salesChannel;
        OrderPriority = orderPriority;
        OrderDate = orderDate;
        OrderID = orderID;
        ShipDate = shipDate;
        UnitsSold = unitsSold;
        UnitPrice = unitPrice;
        UnitCost = unitCost;
    }
}