namespace DeliveryService.Model;

public struct OrderDto
{
    public required int Id { get; set; }
    public required double Weight { get; set; }
    public required string District { get; set; }
    public required DateTime DeliveryTime { get; set; }
}