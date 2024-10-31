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
            logger.Error($"Could not start working with the file at the path: {path}, error message: {ex.Message}");
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
                logger.Error($"Error reading a line from a file along the path: {path}, error message: {ex.Message}");
                throw;
            }

            if (order is not null)
            {
                countRecords++;
                logger.Information($"Order read: {order}, path: {path}");

                yield return order;
            }
        }
        while (order != null);

        logger.Information($"Count of orders read: {countRecords}, path: {path}");
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
            logger.Error($"Could not start working with the file at the path: {path}, error message: {ex.Message}");
            throw;
        }

        await foreach (var order in orders)
        {
            countRecords++;
            try
            {
                await streamWriter.WriteLineAsync(order.ToString());
                logger.Information($"The order has been write: {order}, path: {path}");
            }
            catch
            {
                logger.Error($"Error writing the order: {order}, path: {path}");
            }
        }

        logger.Information($"Count of orders write: {countRecords}, path: {path}");
        streamWriter.Dispose();
    }
}