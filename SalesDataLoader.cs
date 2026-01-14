namespace SalesDataAnalyzer;

public static class SalesDataLoader{
    private static int NumItemsInRow = 11;

    public static List<SalesData> Load(string salesDataFilePath) {
        List<SalesData> salesDataList = new List<SalesData>();

        try
        {
            using (StreamReader reader = new StreamReader(salesDataFilePath))
            {
                int lineNumber = 0;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine()!;
                    lineNumber++;
                    if (lineNumber == 1) continue;

                    string[] values = line.Split(',')!;

                    if (values.Length != NumItemsInRow)
                    {
                        throw new Exception($"Row {lineNumber} contains {values.Length} values. It should contain {NumItemsInRow}.");
                    }
                    try
                    {
                        string region = values[0];
                        string country = values[1];
                        string itemType = values[2];
                        string salesChannel = values[3];
                        string orderPriority = values[4];
                        DateTime orderDate = DateTime.Parse(values[5]);
                        long orderID = Int32.Parse(values[6]);
                        DateTime shipDate = DateTime.Parse(values[7]);
                        int unitsSold = Int32.Parse(values[8]);
                        double unitPrice = Double.Parse(values[9]);
                        double unitCost = Double.Parse(values[10]);
                        SalesData salesData = new SalesData(region, country, itemType, salesChannel, orderPriority, orderDate, orderID, shipDate, unitsSold, unitPrice, unitCost);
                        salesDataList.Add(salesData);
                    }
                    catch (FormatException e)
                    {
                        throw new Exception($"Row {lineNumber} contains invalid data. ({e.Message})");
                    }
                }
            }
        } catch (Exception e){
            throw new Exception($"Unable to open {salesDataFilePath} ({e.Message}).");
        }

        return salesDataList;
    }
}