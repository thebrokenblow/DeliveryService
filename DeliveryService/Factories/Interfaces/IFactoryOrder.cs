using DeliveryService.Model;

namespace DeliveryService.Factories.Interfaces;

public interface IFactoryOrder
{
    Order Create(OrderDto orderDto);
}