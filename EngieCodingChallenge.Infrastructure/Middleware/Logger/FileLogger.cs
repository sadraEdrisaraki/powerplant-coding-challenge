using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;

namespace EngieCodingChallenge.Infrastructure.Middleware.Logger
{
    public static class FileLogger
    {
        public static void CreateLogger()
        {
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                            .MinimumLevel.Debug()
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                            .Enrich.FromLogContext()
                            .CreateLogger();
        }
    }
}
