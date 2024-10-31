using DeliveryService.Core.Validations.Interfaces;
using DeliveryService.Model;

namespace DeliveryService.Core.Validations;

public class ValidationFilePathLog : IValidationArg
{
    public ArgsState SetValidValue(string value, ArgsState argsState)
    {
        value = value.Trim();

        if (!string.IsNullOrEmpty(value))
        {
            argsState.FilePathLog = value;
        }

        return argsState;
    }
}