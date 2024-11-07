using Serilog;

namespace DeliveryService.Core;

public class ConfigurationLogger
{
    public static ILogger Create(string filePath) =>
        new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(filePath)
            .CreateLogger();
}