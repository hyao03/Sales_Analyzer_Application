namespace SalesDataAnalyzer;

class Program
{
    static void Main(string[] args)
    {
        string salesDataFilePath;
        string reportFilePath;

        if (args.Length > 2) {
            Console.WriteLine("SalesDataAnalyzer <sales_data_file_path> <report_file_path>");
            Console.WriteLine("Too many arguments provided. Exiting...");
            Environment.Exit(1);
        }
        else if (args.Length < 2) {
            Console.WriteLine("SalesDataAnalyzer <sales_data_file_path> <report_file_path>");

            Console.WriteLine("Not enough arguments provided. Type sales data file path: ");
            while (true)
            {
                salesDataFilePath = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(salesDataFilePath) && File.Exists(salesDataFilePath) && salesDataFilePath.EndsWith(".csv"))
                {
                    break;
                }
                Console.WriteLine("Sales Data file must end in .csv");
            }
            Console.WriteLine("Type report file path: ");
            while (true)
            {
                reportFilePath = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(reportFilePath) && reportFilePath.EndsWith(".txt"))
                {
                    break;
                }
                Console.WriteLine("Report file must end in .txt");
            }
        } else
        {
            salesDataFilePath = args[0];
            reportFilePath = args[1];
            if (!salesDataFilePath.EndsWith(".csv")) {
                Console.WriteLine("Sales Data file must end in .csv");
                while (true)
                {
                    Console.WriteLine("Type sales data file path: ");
                    salesDataFilePath = Console.ReadLine()!;
                    if (!string.IsNullOrEmpty(salesDataFilePath) && File.Exists(salesDataFilePath) && salesDataFilePath.EndsWith(".csv"))
                    {
                        break;
                    }
                    Console.WriteLine("Sales Data file must end in .csv");
                }
            }
            if (!reportFilePath.EndsWith(".txt")) {
                Console.WriteLine("Report file must end in .txt");
                while (true)
                {
                    Console.WriteLine("Type report file path: ");
                    reportFilePath = Console.ReadLine()!;
                    if (!string.IsNullOrEmpty(reportFilePath) && Path.GetExtension(reportFilePath) == ".txt")
                    {
                        break;
                    }
                    Console.WriteLine("Report file must end in .txt");
                }
            }

            List<SalesData> salesDataList = null!;
            try
            {
                salesDataList = SalesDataLoader.Load(salesDataFilePath);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Environment.Exit(2);
            }

            var report = SalesDataReport.GenerateReport(salesDataList);

            try
            {
                using(StreamWriter reportWriter = new StreamWriter(reportFilePath)){
                    reportWriter.Write(report);
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Environment.Exit(3);
            }
        }
    }
}
