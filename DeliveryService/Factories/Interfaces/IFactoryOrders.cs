using DeliveryService.Model;

namespace DeliveryService.Factories.Interfaces;

public interface IFactoryOrders
{
    public IAsyncEnumerable<Order> CreateOrdersAsync(string path);
}