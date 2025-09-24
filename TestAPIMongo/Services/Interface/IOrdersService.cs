using TestAPIMongo.Data.Models;

namespace TestAPIMongo.Services.Interface
{
    public interface IOrdersService
    {
        Task<(List<OrdersModel>, long)> GetOrdersList(OrdersFilterModel filterModel);
    }
}
