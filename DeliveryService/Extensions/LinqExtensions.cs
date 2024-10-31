namespace DeliveryService.Extensions;

public static class LinqExtensions
{
    public static T Second<T>(this IEnumerable<T> source) =>
        source.Skip(1).First();
}