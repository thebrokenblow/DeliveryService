using DeliveryService.Core.Validations.Interfaces;
using DeliveryService.Model;
using System.Globalization;

namespace DeliveryService.Core.Validations;

public class ValidationFirstDeliveryDateTime : IValidationArg
{
    private const string formatDateTime = "yyyy-MM-dd HH:mm:ss";
    private const string nameCultureInfo = "ru-RU";

    public ArgsState SetValidValue(string value, ArgsState argsState)
    {
        if (DateTime.TryParseExact(
                                value,
                                formatDateTime,
                                new CultureInfo(nameCultureInfo),
                                DateTimeStyles.None,
                                out DateTime deliveryTime))
        {
            argsState.DeliveryDateTime = deliveryTime;
        }

        return argsState;
    }
}