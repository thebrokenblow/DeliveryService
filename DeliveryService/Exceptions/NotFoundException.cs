namespace DeliveryService.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
}