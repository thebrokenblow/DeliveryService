using DeliveryService.Core;
using DeliveryService.Repositories;

var filteredParaments = new FilteredParaments
{
    CityDistrict = "Central",
    FirstDeliveryDateTime = DateTime.Parse("2023-12-10 10:40:00"),
};

var filteredOrders = new FilteredOrders("C:\\Users\\Artem\\Desktop\\data\\data.txt");
var orders = filteredOrders.FilterOrder(filteredParaments);

var repositoryFileOrders = new RepositoryFileOrders();
await repositoryFileOrders.WriteOrdersAsync("C:\\Users\\Artem\\Desktop\\data\\result.txt", orders);

