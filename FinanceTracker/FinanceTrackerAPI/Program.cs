using FinanceTrackerAPI.Models;
using FinanceTrackerAPI.Services;
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
        builder.Services.AddScoped<FinanceTrackerDbService>();

        builder.Services.AddControllers();

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}
