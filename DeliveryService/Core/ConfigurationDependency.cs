using DeliveryService.Factories;
using DeliveryService.Factories.Interfaces;
using DeliveryService.Mapping;
using DeliveryService.Mapping.Interfaces;
using DeliveryService.Repositories;
using DeliveryService.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryService.Core;

public class ConfigurationDependency
{
    public static IServiceProvider GetServiceProvider(string filePath)
    {
        var logger = ConfigurationLogger.Create(filePath);

        var serviceCollection = new ServiceCollection()
            .AddSingleton(logger)
            .AddTransient<IRepositoryFileOrders, RepositoryFileOrders>()
            .AddTransient<IFactoryOrder, FactoryOrder>()
            .AddTransient<IOrderMapper, OrderMapper>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        return serviceProvider;
    }
}