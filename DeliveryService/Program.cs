using Serilog;
using DeliveryService.Core;
using DeliveryService.Repositories;
using DeliveryService.Factories;
using Microsoft.Extensions.DependencyInjection;
using DeliveryService.Repositories.Interfaces;
using DeliveryService.Factories.Interfaces;
using DeliveryService.Core.Interfaces;
using DeliveryService.Core.Validations;


if (args.Length != 5)
{
    throw new InvalidDataException("Неверное количество аргументов");
}

var cityDistrict = new ValidationCityDistrict()
    .GetCorrect(args[0]);

var firstDeliveryDateTime = new ValidationFirstDeliveryDateTime()
    .GetCorrect(args[1]);

var filePathSourceOrder = new ValidationFilePathSourceOrder()
    .GetCorrect(args[2]);

var filePathDeliveryLog = new ValidationFilePathDeliveryLog()
    .GetCorrect(args[3]);

var filePathDeliveryOrder = new ValidationFilePathDeliveryOrder()
    .GetCorrect(args[4]);


var filteredParaments = new FilteredParaments
{
    CityDistrict = cityDistrict,
    FirstDeliveryDateTime = firstDeliveryDateTime,
};

var loggerConfiguration = new LoggerConfiguration()
                                .MinimumLevel.Debug()
                                .WriteTo.File(filePathDeliveryLog)
                                .CreateLogger();

var services = new ServiceCollection()
    .AddSingleton<ILogger>(serviceProvider => loggerConfiguration)
    .AddTransient<IRepositoryFileOrders, RepositoryFileOrders>()
    .AddTransient<IFactoryOrders, FactoryOrders>()
    .AddTransient<IFilteredOrders, FilteredOrders>();

var serviceProvider = services.BuildServiceProvider();

var filteredOrders = serviceProvider.GetRequiredService<IFilteredOrders>();
var orders = filteredOrders.FilterOrderAsync(filePathSourceOrder, filteredParaments);

var repositoryFileOrders = serviceProvider.GetRequiredService<IRepositoryFileOrders>();
await repositoryFileOrders.WriteOrdersAsync(filePathDeliveryOrder, orders);