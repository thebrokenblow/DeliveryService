namespace DeliveryService.Model;

public class Order
{
    public required int Id { get; set; }
    public required double Weight { get; set; }
    public required string District { get; set; }
    public required DateTime DeliveryTime { get; set; }

    public override string ToString() =>
        $"{Id} :: {Weight} :: {District} :: {DeliveryTime}";
}
