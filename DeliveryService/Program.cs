using DeliveryService.Core.CommandLine;
using DeliveryService.Factories;
using DeliveryService.Mapping;
using DeliveryService.Model;
using DeliveryService.Repositories;
using Serilog;

var argsState = new ArgsCommandLine().Validate(args);

var filteringArguments = new FilteringArguments
{
    District = argsState.District,
    FirstDeliveryDateTime = argsState.DeliveryDateTime.Value,
    SecondDeliveryDateTime = argsState.DeliveryDateTime.Value.AddMinutes(30)
};

var logger = new LoggerConfiguration()
                                .MinimumLevel.Debug()
                                .WriteTo.File(argsState.FilePathLog)
                                .CreateLogger();

var repositoryFileOrders = new RepositoryFileOrders(logger);
var ordersStr = repositoryFileOrders.ReadOrdersAsync(argsState.FilePathOrder);
var orderMapper = new OrderMapper(new FactoryOrders(), logger);
var orders = orderMapper.Map(ordersStr);

var filteringOrders = orders.Where(CreateFilteringMethod(filteringArguments));
await repositoryFileOrders.WriteOrdersAsync(argsState.FilePathFilterOrder, filteringOrders);

Func<Order, bool> CreateFilteringMethod(FilteringArguments filteringArguments)
{
    bool filteringDistrict(Order order) =>
                order.District
                    .Equals(filteringArguments.District?
                                        .ToLower(), StringComparison.CurrentCultureIgnoreCase);

    bool filteringDeliveryTime(Order order) =>
                                    order.DeliveryTime >= filteringArguments.FirstDeliveryDateTime &&
                                        order.DeliveryTime <= filteringArguments.SecondDeliveryDateTime;
    return (Order order) =>
                filteringDistrict(order) && filteringDeliveryTime(order);
}