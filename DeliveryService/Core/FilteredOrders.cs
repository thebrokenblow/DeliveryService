using DeliveryService.Model;
using DeliveryService.Core.Interfaces;
using DeliveryService.Factories.Interfaces;

namespace DeliveryService.Core;

public class FilteredOrders(IFactoryOrders factoryOrders) : IFilteredOrders
{
    public IAsyncEnumerable<Order> FilterOrderAsync(string path, FilteredParaments filteredParaments)
    {
        var rightBorderDateFiltering = filteredParaments.FirstDeliveryDateTime.AddMinutes(30);

        return factoryOrders
                    .CreateOrdersAsync(path)
                    .Where(order => (order.District == filteredParaments.CityDistrict) &&
                                    (order.DeliveryTime >= filteredParaments.FirstDeliveryDateTime && 
                                        order.DeliveryTime <= rightBorderDateFiltering));
    }
}