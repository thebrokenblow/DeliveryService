using DeliveryService.Model;
using DeliveryService.Factories;

namespace DeliveryService.Core;

public class FilteredOrders(string path)
{
    private readonly FactoryOrders factoryOrders = new(path);

    public IAsyncEnumerable<Order> FilterOrder(FilteredParaments filteredParaments)
    {
        return factoryOrders
            .CreateOrders()
            .Where(x => (x.District == filteredParaments.CityDistrict) &&
                 (x.DeliveryTime >= filteredParaments.FirstDeliveryDateTime && 
                  x.DeliveryTime <= filteredParaments.FirstDeliveryDateTime.AddMinutes(30)));
    }
}