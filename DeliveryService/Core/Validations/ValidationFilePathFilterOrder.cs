using DeliveryService.Core.Validations.Interfaces;
using DeliveryService.Model;
using Serilog;

namespace DeliveryService.Core.Validations;

public class ValidationFilePathFilterOrder(ILogger logger) : IValidationArg
{
    public ArgsState SetValidValue(string value, ArgsState argsState)
    {
        value = value.Trim();

        if (!string.IsNullOrEmpty(value))
        {
            argsState.FilePathFilterOrder = value;
            logger.Information($"The value was entered from the command line of the path filtered orders: {value}");
        }
        else
        {
            logger.Error($"The incorrect value was entered for the filtered orders path, the value of the path filtered orders: {value}");
            throw new ArgumentException($"The Incorrect value for the path filtered orders: {value}");
        }

        return argsState;
    }
}