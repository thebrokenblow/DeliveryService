using DeliveryService.Model;
using DeliveryService.Core.Validations.Interfaces;

namespace DeliveryService.Core.Validations;

public class ValidationCityDistrict : IValidationArg
{
    public ArgsState SetValidValue(string value, ArgsState argsState)
    {
        value = value.Trim();

        if (!string.IsNullOrEmpty(value))
        {
            argsState.District = value;
        }
        else
        {
            throw new ArgumentException($"The Incorrect value for district: {value}");
        }

        return argsState;
    }
}