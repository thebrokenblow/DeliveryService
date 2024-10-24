using DeliveryService.Model;

namespace DeliveryService.Core.Interfaces;

public interface IFilteredOrders
{
    public IAsyncEnumerable<Order> FilterOrderAsync(string path, FilteredParaments filteredParaments);
}