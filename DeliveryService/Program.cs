using DeliveryService.Core;
using DeliveryService.Core.CommandLine;
using DeliveryService.Mapping.Interfaces;
using DeliveryService.Model;
using DeliveryService.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var argsState = new ArgsCommandLine().Validate(args);

var filteringArguments = new FilteringArguments
{
    District = argsState.District!,
    FirstDeliveryDateTime = argsState.FirstDeliveryDateTime!.Value,
    SecondDeliveryDateTime = argsState.FirstDeliveryDateTime.Value.AddMinutes(30)
};

var serviceProvider = ConfigurationDependency.GetServiceProvider(argsState.FilePathLog!);

var orderMapper = serviceProvider.GetRequiredService<IOrderMapper>();
var repositoryFileOrders = serviceProvider.GetRequiredService<IRepositoryFileOrders>();
var ordersStr = repositoryFileOrders.ReadOrdersAsync(argsState.FilePathOrder!);

var orders = orderMapper.Map(ordersStr);

var filteringOrders = orders.Where(ConfigureFiltering.CreateFiltering(filteringArguments));
await repositoryFileOrders.WriteOrdersAsync(argsState.FilePathFilterOrder!, filteringOrders);