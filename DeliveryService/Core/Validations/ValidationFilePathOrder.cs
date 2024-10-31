using DeliveryService.Core.Validations.Interfaces;
using DeliveryService.Model;

namespace DeliveryService.Core.Validations;

public class ValidationFilePathOrder : IValidationArg
{
    public ArgsState SetValidValue(string value, ArgsState argsState)
    {
        value = value.Trim();

        if (!string.IsNullOrEmpty(value))
        {
            argsState.FilePathOrder = value;
        }
        else
        {
            throw new ArgumentException($"The Incorrect value for the orders path: {value}");
        }

        return argsState;
    }
}