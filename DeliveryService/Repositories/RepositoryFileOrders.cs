using Serilog;
using DeliveryService.Model;
using DeliveryService.Repositories.Interfaces;
using System.IO.IsolatedStorage;

namespace DeliveryService.Repositories;

public class RepositoryFileOrders(ILogger logger) : IRepositoryFileOrders
{
    public async IAsyncEnumerable<string> ReadOrdersAsync(string path)
    {
        StreamReader? streamReader;
        try
        {
            streamReader = new StreamReader(path);
        }
        catch(Exception ex)
        {
            logger.Error($"Не смогли начать работу с файлом по пути: {path}, сообщение ошибки: {ex.Message}");
            throw;
        }
        
        string? order;
        int countRecords = 0;
        do
        {
            try
            {
                order = await streamReader.ReadLineAsync();
            }
            catch(Exception ex)
            {
                logger.Error($"Ошибка чтения строки из файла по пути: {path}, сообщение ошибки: {ex.Message}");
                throw;
            }

            logger.Information($"Прочтён заказ ({order}) путь ({path})");

            if (order is not null)
            {
                countRecords++;

                yield return order;
            }
        }
        while (order != null);

        logger.Information($"Прочтённых заказов ({countRecords}) по пути ({path})");
        streamReader.Dispose();
    }

    public async Task WriteOrdersAsync(string path, IAsyncEnumerable<Order> orders)
    {
        var isolatedStorageFileStream = new IsolatedStorageFileStream(
                 path,
                 FileMode.Truncate);

        int countRecords = 0;
        StreamWriter? streamWriter;
        try
        {
            streamWriter = new StreamWriter(isolatedStorageFileStream);
        }
        catch(Exception ex)
        {
            logger.Error($"Не смогли начать работу с файлом по пути: {path}, сообщение ошибки: {ex.Message}");
            throw;
        }

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

        streamWriter.Dispose();
        logger.Information($"Записано заказов ({countRecords}) по пути ({path})");
    }
}