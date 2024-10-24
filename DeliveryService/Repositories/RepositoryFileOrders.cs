using DeliveryService.Model;
using System.IO.IsolatedStorage;

namespace DeliveryService.Repositories;

public class RepositoryFileOrders
{
    public async IAsyncEnumerable<string> ReadOrdersAsync(string path)
    {
        using var streamReader = new StreamReader(path);
        string? order;
        while ((order = await streamReader.ReadLineAsync()) != null)
        {
            yield return order;
        }
    }

    public async Task WriteOrdersAsync(string path, IAsyncEnumerable<Order> orders)
    {
        var isolatedStorageFileStream = new IsolatedStorageFileStream(
                 path,
                 FileMode.Truncate);

        using var streamWriter = new StreamWriter(isolatedStorageFileStream);

        await foreach (var order in orders)
        {
            await streamWriter.WriteLineAsync(order.ToString());
        }
    }
}