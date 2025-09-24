using TestAPIMongo.Data.Models;

namespace TestAPIMongo.Services.Interface
{
    public interface IOrdersService
    {
        Task<(IEnumerable<OrderModel>, long)> GetOrdersList(OrdersFilterModel filterModel);
    }
}
