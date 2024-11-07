using DeliveryService.Model;

namespace DeliveryService.Core;

public class ConfigureFiltering
{
    public static Func<Order, bool> CreateFiltering(FilteringArguments filteringArguments)
    {
        bool filteringDistrict(Order order) =>
                    order.District
                        .Equals(filteringArguments.District?
                                            .ToLower(), StringComparison.CurrentCultureIgnoreCase);

        bool filteringDeliveryTime(Order order) =>
                                        order.DeliveryTime >= filteringArguments.FirstDeliveryDateTime &&
                                            order.DeliveryTime <= filteringArguments.SecondDeliveryDateTime;
        return (order) =>
                    filteringDistrict(order) && filteringDeliveryTime(order);
    }
}