namespace DeliveryService.Core.Validations;

public class ValidationFilePathDeliveryLog
{
    private const string correctTitleVariable = "_deliveryLog";

    public string GetCorrect(string arg)
    {
        var variableValue = arg.Split("=");

        var titleVariable = variableValue[0];
        var value = variableValue[1];

        if (IsValid(titleVariable, value))
        {
            return value;
        }

        throw new InvalidDataException("Некорректный путь к файлу");
    }

    private static bool IsValid(string titleVariable, string value) =>
        titleVariable == correctTitleVariable &&
                !string.IsNullOrEmpty(value.Trim());
}
