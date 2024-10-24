using DeliveryService.Model;
using DeliveryService.Repositories;

namespace DeliveryService.Factories;

public class FactoryOrders(string path)
{
    private readonly RepositoryFileOrders repositoryFileOrders = new();

    public async IAsyncEnumerable<Order> CreateOrders()
    {
        await foreach (var stringOrder in repositoryFileOrders.ReadOrdersAsync(path))
        {
            var order = CreateOrder(stringOrder);

            yield return order;
        }
    }

    private static Order CreateOrder(string stringOrder)
    {
        var paramsOrder = stringOrder.Split("::")
                                     .Select(param => param.Trim())
                                     .ToList();

        return new Order
        {
            Id = int.Parse(paramsOrder[0]),
            Weight = double.Parse(paramsOrder[1]),
            District = paramsOrder[2],
            DeliveryTime = DateTime.ParseExact
                           (paramsOrder[3],
                            "yyyy-MM-dd HH:mm:ss",
                            System.Globalization.CultureInfo.InvariantCulture)
        };
    }
}