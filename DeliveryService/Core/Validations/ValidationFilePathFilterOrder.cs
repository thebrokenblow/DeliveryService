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

        return argsState;
    }
}