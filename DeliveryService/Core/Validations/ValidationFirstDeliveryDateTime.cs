using System.Globalization;

namespace DeliveryService.Core.Validations;

public class ValidationFirstDeliveryDateTime
{
    private const string correctTitleVariable = "_firstDeliveryDateTime";

    public DateTime GetCorrect(string arg)
    {
        var variableValue = arg.Split("=");

        var titleVariable = variableValue[0];
        var value = variableValue[1];

        if (titleVariable != correctTitleVariable)
        {
            throw new InvalidDataException("Некорректно задано название переменной");
        }

        if (DateTime.TryParseExact(value,
                             "yyyy-MM-dd HH:mm:ss",
                              new CultureInfo("ru-RU"),
                              DateTimeStyles.None,
                              out DateTime dateValue))
        {
            return dateValue;
        }

        throw new InvalidDataException("Некорректная дата");
    }
}