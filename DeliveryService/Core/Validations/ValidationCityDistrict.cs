using DeliveryService.Model;
using DeliveryService.Core.Validations.Interfaces;
using Serilog;

namespace DeliveryService.Core.Validations;

public class ValidationCityDistrict(ILogger logger) : IValidationArg
{
    public ArgsState SetValidValue(string value, ArgsState argsState)
    {
        value = value.Trim();

        if (!string.IsNullOrEmpty(value))
        {
            argsState.District = value;
            logger.Information($"The value was entered from the command line of the district: {value}");
        }
        else
        {
            logger.Error($"The incorrect value was entered for the district, the value of the district: {value}");
            throw new ArgumentException($"The Incorrect value for district: {value}");
        }

        return argsState;
    }
}