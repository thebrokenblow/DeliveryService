using DeliveryService.Core.Validations.Interfaces;
using DeliveryService.Model;
using Serilog;

namespace DeliveryService.Core.Validations;

public class ValidationFilePathLog(ILogger logger) : IValidationArg
{
    public ArgsState SetValidValue(string value, ArgsState argsState)
    {
        value = value.Trim();

        if (!string.IsNullOrEmpty(value))
        {
            argsState.FilePathLog = value;
            logger.Information($"The value was entered from the command line of the path logs: {value}");
        }
        else
        {
            logger.Error($"The incorrect value was entered for the path logs, the value of the path logs: {value}");
            throw new ArgumentException($"The Incorrect value for the path logs: {value}");
        }

        return argsState;
    }
}