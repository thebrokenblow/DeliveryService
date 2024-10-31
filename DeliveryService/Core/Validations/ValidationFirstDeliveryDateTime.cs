using DeliveryService.Core.Validations.Interfaces;
using DeliveryService.Model;
using Serilog;
using System.Globalization;

namespace DeliveryService.Core.Validations;

public class ValidationFirstDeliveryDateTime(ILogger logger, string formatDateTime = "yyyy-MM-dd HH:mm:ss", string nameCultureInfo = "ru-RU") : IValidationArg
{
    public ArgsState SetValidValue(string value, ArgsState argsState)
    {
        if (DateTime.TryParseExact(
                                value,
                                formatDateTime,
                                new CultureInfo(nameCultureInfo),
                                DateTimeStyles.None,
                                out DateTime deliveryTime))
        {
            argsState.FirstDeliveryDateTime = deliveryTime;
            logger.Information($"The value was entered from the command line of the date and time first delivery : {value}");
        }
        else
        {
            logger.Error($"The incorrect value was entered for the path orders, the value of the date and time first delivery: {value}");
            throw new ArgumentException($"The Incorrect value for the date of the date and time first delivery: {value}");
        }

        return argsState;
    }
}