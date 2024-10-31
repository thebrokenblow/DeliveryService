using DeliveryService.Model;

namespace DeliveryService.Repositories.Interfaces;

public interface IRepositoryFileOrders
{
    IAsyncEnumerable<string> ReadOrdersAsync(string path);
    Task WriteOrdersAsync(string path, IAsyncEnumerable<Order> orders);
}