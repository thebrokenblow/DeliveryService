using DeliveryService.Core.Validations.Interfaces;
using DeliveryService.Model;

namespace DeliveryService.Core.Validations;

public class ValidationFilePathFilterOrder : IValidationArg
{
    public ArgsState SetValidValue(string value, ArgsState argsState)
    {
        value = value.Trim();

        if (!string.IsNullOrEmpty(value))
        {
            argsState.FilePathFilterOrder = value;
        }
        else
        {
            throw new ArgumentException($"The Incorrect value for the path filtered orders: {value}");
        }

        return argsState;
    }
}