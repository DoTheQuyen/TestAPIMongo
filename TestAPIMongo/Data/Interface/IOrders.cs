using TestAPIMongo.Data.Models;

namespace TestAPIMongo.Data.Interface
{
    public interface IOrders
    {
        Task<(List<OrdersModel>, long)> GetOrdersList(OrdersFilterModel filterModel);
    }

}
