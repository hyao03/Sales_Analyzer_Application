namespace SalesDataAnalyzer;

public static class SalesDataReport{
    public static string GenerateReport(List<SalesData> salesDataList)
    {
        string report = "";

        if (salesDataList.Count() < 2)
        {
            report += "No data is available.\n";

            return report;
        }

        //Q1
        report += "******************************************\n";
        report += "Total Profit in Dataset\n";
        report += "******************************************\n";

        if (salesDataList.Count() == 0)
        {
            report += "None.\n";
        } else
        {
            var totalRevenue = salesDataList.Sum(s => s.UnitPrice * s.UnitsSold);
            var costOfItemsSold = salesDataList.Sum(s => s.UnitCost * s.UnitsSold);

            var totalProfit = totalRevenue - costOfItemsSold;
            report += $"Total Profit: ${totalProfit:N2}\n";
        }

        //Q2
        report += "\n******************************************\n";
        report += "Unique Item Type\n";
        report += "******************************************\n";
        var uniqueItemTypes = (from salesData in salesDataList select salesData.ItemType).Distinct();
        foreach (var itemType in uniqueItemTypes)
        {
            report += $"{itemType}\n";
        }

        //Q3
        report += "\n******************************************\n";
        report += "Total Sales per Item Type\n";
        report += "******************************************\n";

        var salesPerItemType = from salesData in salesDataList
                               group salesData by salesData.ItemType into itemTypeGroup
                               orderby itemTypeGroup.Key
                               select new { ItemType = itemTypeGroup.Key, TotalSales = itemTypeGroup.Sum(s => s.UnitsSold * s.UnitPrice) };
        foreach (var itemTypeGroup in salesPerItemType)
        {
            report += $"{itemTypeGroup.ItemType}: ${itemTypeGroup.TotalSales:N2}\n";
        }

        //Q4
        report += "\n******************************************\n";
        report += "Total Sales per Country\n";
        report += "******************************************\n";
        var salesPerCountry = from salesData in salesDataList
                               group salesData by salesData.Country into countryGroup
                               orderby countryGroup.Key
                               select new { Country = countryGroup.Key, TotalSales = countryGroup.Sum(s => s.UnitsSold * s.UnitPrice) };
        foreach (var countryGroup in salesPerCountry)
        {
            report += $"{countryGroup.Country}: ${countryGroup.TotalSales:N2}\n";
        }

        //Q5
        report += "\n******************************************\n";
        report += "Orders with the Highest Sale\n";
        report += "******************************************\n";
        var highestSale = (from salesData in salesDataList select salesData.UnitsSold * salesData.UnitPrice).Max();
        var highestSalesOrders = from salesData in salesDataList where (salesData.UnitsSold * salesData.UnitPrice) == highestSale select salesData;
        foreach (var order in highestSalesOrders)
        {
            report += $"{order.OrderID}: ${highestSale:N2}\n";
        }

        //Q6
        report += "\n******************************************\n";
        report += "Total Sales per Year\n";
        report += "******************************************\n";
        var salesPerYear = from salesData in salesDataList
                           group salesData by salesData.OrderDate.Year into yearGroup
                           orderby yearGroup.Key
                           select new { Year = yearGroup.Key, TotalSales = yearGroup.Sum(s => s.UnitsSold * s.UnitPrice) };
        foreach (var yearGroup in salesPerYear)
        {
            report += $"{yearGroup.Year}: ${yearGroup.TotalSales:N2}\n";
        }

        //Q7
        report += "\n******************************************\n";
        report += "Total Sales per Channel\n";
        report += "******************************************\n";
        var salesPerChannel = from salesData in salesDataList
                              group salesData by salesData.SalesChannel into channelGroup
                              orderby channelGroup.Key
                              select new { SalesChannel = channelGroup.Key, TotalSales = channelGroup.Sum(s => s.UnitsSold * s.UnitPrice) };
        foreach (var channelGroup in salesPerChannel)
        {
            report += $"{channelGroup.SalesChannel}: ${channelGroup.TotalSales:N2}\n";
        }

        //Q8
        report += "\n******************************************\n";
        report += "Total Orders per Priority Type\n";
        report += "******************************************\n";
        var ordersPerPriority = from salesData in salesDataList
                                group salesData by salesData.OrderPriority into priorityGroup
                                orderby priorityGroup.Key
                                select new { OrderPriority = priorityGroup.Key, TotalOrders = priorityGroup.Count() };
        foreach (var priorityGroup in ordersPerPriority)
        {
            report += $"{priorityGroup.OrderPriority}: {priorityGroup.TotalOrders}\n";
        }

        //Q9
        report += "\n******************************************\n";
        report += "Average Sales per Item Type\n";
        report += "******************************************\n";
        var averageSalesPerItemType = from salesData in salesDataList
                                      group salesData by salesData.ItemType into itemTypeGroup
                                      orderby itemTypeGroup.Key
                                      select new { ItemType = itemTypeGroup.Key, AverageSales = itemTypeGroup.Average(s => s.UnitsSold * s.UnitPrice) };
        foreach (var itemTypeGroup in averageSalesPerItemType)
        {
            report += $"{itemTypeGroup.ItemType}: ${itemTypeGroup.AverageSales:N2}\n";
        }

        //Q10
        report += "\n******************************************\n";
        report += "Total Orders per Region\n";
        report += "******************************************\n";
        var salesPerRegion = from salesData in salesDataList
                              group salesData by salesData.Region into regionGroup
                              orderby regionGroup.Key
                              select new { Region = regionGroup.Key, TotalSales = regionGroup.Sum(s => s.UnitsSold) };
        foreach (var regionGroup in salesPerRegion)
        {
            report += $"{regionGroup.Region}: ${regionGroup.TotalSales}\n";
        }

        //Q11
        report += "\n******************************************\n";
        report += "Region Orders Most Beverages\n";
        report += "******************************************\n";
        var beverageOrdersPerRegion = from salesData in salesDataList
                                      where salesData.ItemType == "Beverages"
                                      group salesData by salesData.Region into regionGroup
                                      orderby regionGroup.Count() descending
                                      select new { Region = regionGroup.Key, UnitsSold = regionGroup.Sum(s => s.UnitsSold)};
        var topRegion = beverageOrdersPerRegion.FirstOrDefault();
        if (topRegion != null)
        {
            var beverageOrdersInTopRegion = (from salesData in salesDataList
                                      where salesData.ItemType == "Beverages" && salesData.Region == topRegion.Region
                                      select salesData.UnitsSold).Sum();
            report += $"{topRegion.Region}: {topRegion.UnitsSold}\n";
        } else
        {
            report += "None.\n";
        }

        //Q12
        report += "\n******************************************\n";
        report += "Baby Food Sales per Region\n";
        report += "******************************************\n";
        var babyFoodSalesPerRegion = from salesData in salesDataList
                                      where salesData.ItemType == "Baby Food"
                                      group salesData by salesData.Region into regionGroup
                                      orderby regionGroup.Key
                                      select new { Region = regionGroup.Key, TotalSales = regionGroup.Sum(s => s.UnitsSold * s.UnitPrice) };
        foreach (var regionGroup in babyFoodSalesPerRegion)
        {
            report += $"{regionGroup.Region}: ${regionGroup.TotalSales:N2}\n";
        }
        if (babyFoodSalesPerRegion.Count() == 0)
        {
            report += "None.\n";
        }

        return report;
    }
}