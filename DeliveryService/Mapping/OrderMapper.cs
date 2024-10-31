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
            logger.Error("В записи есть лишние свойства");
        }

        return new OrderDto
        {
            Id = ParseType(enumeratorOrderProperties, int.Parse),
            Weight = ParseType(enumeratorOrderProperties, double.Parse),
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
            logger.Error("Ошибка преобразования типа");
            throw new IncorrectData($"Не удалось преобразовать тип, сообщение ошибки: {exception.Message}");
        }
    }

    private void LogErrorReadingProperty()
    {
        logger.Error("Свойство не было прочитано");
        throw new NotFoundException("Свойство не было прочитано");
    }
}