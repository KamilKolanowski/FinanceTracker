using Avalonia;
using System;
using System.Threading.Tasks;
using FinanceTracker.Services;

namespace FinanceTracker
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // Run async logic before starting UI
            Task.Run(async () => await RunApiTest()).GetAwaiter().GetResult();

            // Start Avalonia app
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();

        private static async Task RunApiTest()
        {
            var api = new ExpensesApi();
            try
            {
                var result = await api.GetExpensesAsync();
                foreach (var expense in result)
                {
                    Console.WriteLine(expense.ExpenseDate.ToString("dd/MM/yyyy HH:mm:ss"));
                }
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("API call failed: " + ex.Message);
            }
        }
    }
}