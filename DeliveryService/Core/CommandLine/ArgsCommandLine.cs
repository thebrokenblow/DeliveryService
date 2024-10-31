using DeliveryService.Core.Validations;
using DeliveryService.Core.Validations.Interfaces;
using DeliveryService.Extensions;
using DeliveryService.Model;
using Serilog;

namespace DeliveryService.Core.CommandLine;

public class ArgsCommandLine(ILogger logger)
{
    private const int countElementsArgument = 2;
    private const char assignmentOperator = '=';

    private readonly Dictionary<string, IValidationArg> argsValidation = new()
    {
        { "_cityDistrict", new ValidationCityDistrict(logger) },
        { "_firstDeliveryDateTime", new ValidationFirstDeliveryDateTime(logger) },
        { "_sourceOrder", new ValidationFilePathOrder(logger) },
        { "_deliveryLog", new ValidationFilePathLog(logger) },
        { "_deliveryOrder", new ValidationFilePathFilterOrder(logger) }
    };

    public ArgsState Validate(string[] args)
    {
        if (args.Length != argsValidation.Count)
        {
            logger.Error("Incorrect number of arguments entered");
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
                logger.Error($"Incorrect argument name has been entered: {nameParameter}");
                throw new ArgumentException($"Incorrect argument name has been entered: {nameParameter}");
            }
        }

        return argsState;
    }
}