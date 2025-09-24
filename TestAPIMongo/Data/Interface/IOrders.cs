using TestAPIMongo.Data.Models;

namespace TestAPIMongo.Data.Interface
{
    public interface IOrders
    {
        Task<(IEnumerable<OrderModel>, long)> GetOrdersList(OrdersFilterModel filterModel);
    }

}
