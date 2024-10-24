using Serilog;
using DeliveryService.Model;
using DeliveryService.Repositories.Interfaces;
using System.IO.IsolatedStorage;

namespace DeliveryService.Repositories;

public class RepositoryFileOrders(ILogger logger) : IRepositoryFileOrders
{
    public async IAsyncEnumerable<string> ReadOrdersAsync(string path)
    {
        using var streamReader = new StreamReader(path);
        
        string? order;
        int countRecords = 0;

        do
        {
            order = await streamReader.ReadLineAsync();

            logger.Information($"Прочтён заказ ({order}) путь ({path})");
            countRecords++;

            if (order is not null)
            {
                yield return order;
            }
        }
        while (order != null);

        logger.Information($"Прочтённых заказов ({countRecords}) по пути ({path})");
    }

    public async Task WriteOrdersAsync(string path, IAsyncEnumerable<Order> orders)
    {
        var isolatedStorageFileStream = new IsolatedStorageFileStream(
                 path,
                 FileMode.Truncate);

        int countRecords = 0;
        using var streamWriter = new StreamWriter(isolatedStorageFileStream);

        await foreach (var order in orders)
        {
            countRecords++;
            try
            {
                await streamWriter.WriteLineAsync(order.ToString());
                logger.Information($"Записан заказ ({order}) путь ({path})");
            }
            catch
            {
                logger.Error($"Ошибка записи заказа ({order}) путь ({path})");
            }
        }

        logger.Information($"Записано заказов ({countRecords}) по пути ({path})");
    }
}