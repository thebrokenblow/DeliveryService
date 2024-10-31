namespace DeliveryService.Model;

public class FilteringArguments
{
    public required string District { get; set; }
    public required DateTime FirstDeliveryDateTime { get; set; }
    public required DateTime SecondDeliveryDateTime { get; set; }
}