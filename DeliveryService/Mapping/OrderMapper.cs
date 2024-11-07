using Serilog;
using System.Globalization;
using DeliveryService.Model;
using DeliveryService.Exceptions;
using DeliveryService.Mapping.Interfaces;
using DeliveryService.Factories.Interfaces;

namespace DeliveryService.Mapping;

public class OrderMapper(
    IFactoryOrder factoryOrder, 
    ILogger logger, 
    string formatDateTime = "yyyy-MM-dd HH:mm:ss",
    string nameCultureInfo = "ru-RU") : IOrderMapper
{
    public IAsyncEnumerable<Order> Map(IAsyncEnumerable<string> orders) =>
        orders.Select(order => factoryOrder.Create(ParseOrder(order)));

    private OrderDto ParseOrder(string order)
    {
        var orderProperties = order.Split("::")
                                   .Select(parameter => parameter.Trim());

        var enumeratorOrderProperties = orderProperties.GetEnumerator();

        var id = ParseType(enumeratorOrderProperties, int.Parse);

        var weight = ParseType(enumeratorOrderProperties, double.Parse);
        enumeratorOrderProperties.MoveNext();

        var district = enumeratorOrderProperties.Current;

        if (!enumeratorOrderProperties.MoveNext())
        {
            LogErrorReadingProperty();
        }

        if (!DateTime.TryParseExact(
                                enumeratorOrderProperties.Current,
                                formatDateTime,
                                new CultureInfo(nameCultureInfo),
                                DateTimeStyles.None,
                                out DateTime deliveryTime))
        {
            LogErrorReadingProperty();
        }

        if (enumeratorOrderProperties.MoveNext())
        {
            logger.Error("There are extra properties in the record");

            throw new IncorrectData("There are extra properties in the record");
        }

        return new OrderDto
        {
            Id = id,
            Weight = weight,
            District = district,
            DeliveryTime = deliveryTime,
        };
    }

    private T ParseType<T>(IEnumerator<string> propertyOrder, Func<string, T> validatePropertyOrder)
    {
        if (!propertyOrder.MoveNext())
        {
            LogErrorReadingProperty();
        }

        try
        {
            return validatePropertyOrder.Invoke(propertyOrder.Current);
        }
        catch (Exception exception)
        {
            logger.Error($"Failed to convert the type, error message: {exception.Message}");

            throw new IncorrectData($"Failed to convert the type, error message: {exception.Message}");
        }
    }

    private void LogErrorReadingProperty()
    {
        logger.Error("The property has not been read");

        throw new NotFoundException("The property has not been read");
    }
}