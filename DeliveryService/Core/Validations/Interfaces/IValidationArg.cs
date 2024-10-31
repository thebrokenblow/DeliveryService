using DeliveryService.Model;

namespace DeliveryService.Core.Validations.Interfaces;

public interface IValidationArg
{
    ArgsState SetValidValue(string value, ArgsState argsState);
}
