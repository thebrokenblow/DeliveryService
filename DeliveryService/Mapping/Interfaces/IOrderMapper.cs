using DeliveryService.Model;

namespace DeliveryService.Mapping.Interfaces;

public interface IOrderMapper
{
    IAsyncEnumerable<Order> Map(IAsyncEnumerable<string> orders);
}