using DeliveryService.Model;
using DeliveryService.Factories.Interfaces;

namespace DeliveryService.Factories;

public class FactoryOrders : IFactoryOrder
{
    public Order Create(OrderDto orderDto) =>
        new()
        {
            Id = orderDto.Id,
            Weight = orderDto.Weight,
            District = orderDto.District,
            DeliveryTime = orderDto.DeliveryTime,
        };
}