using FinanceTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<FinanceTrackerDbContext>(opt =>
            opt.UseSqlite(builder.Configuration.GetConnectionString("FinanceTrackerDb"))
        );

        var app = builder.Build();

        app.Run();
    }
}
