using DeliveryService.Core.Validations;
using DeliveryService.Core.Validations.Interfaces;
using DeliveryService.Extensions;
using DeliveryService.Model;
using Serilog;

namespace DeliveryService.Core.CommandLine;

public class ArgsCommandLine
{
    private const int countElementsArgument = 2;
    private const char assignmentOperator = '=';

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
        if (args.Length != argsValidation.Count)
        {
            throw new ArgumentException("Incorrect number of arguments");
        }

        var argsState = new ArgsState();

        foreach (var arg in args)
        {
            var currentArgs = arg.Split(assignmentOperator, countElementsArgument);
            var nameParameter = currentArgs.First();

            if (argsValidation.TryGetValue(nameParameter, out IValidationArg? validationArg))
            {
                validationArg.SetValidValue(currentArgs.Second(), argsState);
            }
            else
            {
                throw new ArgumentException($"Incorrect argument name has been entered: {nameParameter}");
            }
        }

        return argsState;
    }
}