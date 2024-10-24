using DeliveryService.Model;
using DeliveryService.Factories.Interfaces;
using DeliveryService.Repositories.Interfaces;
using Serilog;
using Serilog.Core;

namespace DeliveryService.Factories;

public class FactoryOrders(IRepositoryFileOrders repositoryFileOrders) : IFactoryOrders
{
    public async IAsyncEnumerable<Order> CreateOrdersAsync(string path)
    {
        await foreach (var stringOrder in repositoryFileOrders.ReadOrdersAsync(path))
        {
            var order = CreateOrder(stringOrder);

            yield return order;
        }
    }

    private static Order CreateOrder(string stringOrder)
    {
        var parametersOrder = stringOrder.Split("::")
                                     .Select(parameter => parameter.Trim())
                                     .ToList();

        return new Order
        {
            Id = int.Parse(parametersOrder[0]),
            Weight = double.Parse(parametersOrder[1]),
            District = parametersOrder[2],
            DeliveryTime = DateTime.ParseExact
                           (parametersOrder[3],
                            "yyyy-MM-dd HH:mm:ss",
                            System.Globalization.CultureInfo.InvariantCulture)
        };
    }
}