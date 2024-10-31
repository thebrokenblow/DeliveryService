using DeliveryService.Core.Validations.Interfaces;
using DeliveryService.Model;
using Serilog;

namespace DeliveryService.Core.Validations;

public class ValidationFilePathOrder(ILogger logger) : IValidationArg
{
    public ArgsState SetValidValue(string value, ArgsState argsState)
    {
        value = value.Trim();

        if (!string.IsNullOrEmpty(value))
        {
            argsState.FilePathOrder = value;
            logger.Information($"The value was entered from the command line of the path orders: {value}");
        }
        else
        {
            logger.Error($"The incorrect value was entered for the path orders, the value of the path orders: {value}");
            throw new ArgumentException($"The Incorrect value for the orders path: {value}");
        }

        return argsState;
    }
}