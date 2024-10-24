using DeliveryService.Model;

namespace DeliveryService.Repositories.Interfaces;

public interface IRepositoryFileOrders
{
    public IAsyncEnumerable<string> ReadOrdersAsync(string path);
    public Task WriteOrdersAsync(string path, IAsyncEnumerable<Order> orders);
}