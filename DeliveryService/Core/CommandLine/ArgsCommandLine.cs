using DeliveryService.Core.Validations;
using DeliveryService.Core.Validations.Interfaces;
using DeliveryService.Extensions;
using DeliveryService.Model;

namespace DeliveryService.Core.CommandLine;

public class ArgsCommandLine
{
    private readonly Dictionary<string, IValidationArg> argsValidation = new()
    {
        { "_cityDistrict", new ValidationCityDistrict() },
        { "_firstDeliveryDateTime", new ValidationFirstDeliveryDateTime() },
        { "_sourceOrder", new ValidationFilePathOrder() },
        { "_deliveryLog", new ValidationFilePathLog() },
        { "_deliveryOrder", new ValidationFilePathFilterOrder() }
    };

    public ArgsState Validate(string[] args)
    {
        if (args.Length != 5)
        {
            throw new InvalidDataException("Неверное количество аргументов");
        }

        var argsState = new ArgsState();

        foreach (var arg in args)
        {
            var currentArgs = arg.Split('=', 2);
            var nameParameter = currentArgs.First();

            if (argsValidation.TryGetValue(nameParameter, out IValidationArg? validationArg))
            {
                validationArg.SetValidValue(currentArgs.Second(), argsState);
            }
            else
            {
                //Тут исключение
            }
        }

        return argsState;
    }
}