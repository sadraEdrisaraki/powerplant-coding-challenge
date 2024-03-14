using EngieCodingChallenge.Core.v1.UnitCommitCalculation;
using EngieCodingChallenge.Infrastructure.Middleware.Logger;
using Serilog;

public class Program
{

    public async static Task Main(string[] args)
    {
        
        try
        {

            FileLogger.CreateLogger();

            Log.Information("Starting up");
            var builder = WebApplication.CreateBuilder(args);
           
            builder.WebHost.UseUrls("http://localhost:8888");
            
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddTransient<IUnitCommitCalculation, UnitCommitCalculation>();
            //builder.Services.AddApiVersioning();
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }
            
        

    }


}