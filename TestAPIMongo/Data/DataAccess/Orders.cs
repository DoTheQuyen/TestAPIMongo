using Microsoft.Extensions.Options;
using System.Text.Json;
using TestAPIMongo.Data.Interface;
using TestAPIMongo.Data.Models;

namespace TestAPIMongo.Data.DataAccess
{

    /// <summary>
    /// The data layer is only used for query, create, update, delete data in DB
    /// Do not process any business rule in this layer
    /// </summary>
    public class Orders : IOrders
    {
        private readonly List<OrderModel> _ordersInMemory;

        public Orders(IOptions<DatabaseSetting> dbSettings)
        {
            var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "sample-orders.json");
            if (!File.Exists(jsonFilePath))
            {
                _ordersInMemory = new List<OrderModel>();
            }
            else
            {
                var jsonString = File.ReadAllText(jsonFilePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                _ordersInMemory = JsonSerializer.Deserialize<List<OrderModel>>(jsonString, options)
                                  ?? new List<OrderModel>();
                foreach (var order in _ordersInMemory)
                {
                    if (DateTime.TryParse(order.CreatedAt, out var parsedDate))
                    {
                        // Replace string with normalized ISO string
                        order.CreatedAt = parsedDate.ToString("o");
                    }
                }
            }
        }

        /// <summary>
        /// GetOrdersList to filter the list of orders base on filter model
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<(IEnumerable<OrderModel>, long)> GetOrdersList(OrdersFilterModel filterModel)
        {
            try
            {
                var query = _ordersInMemory.AsQueryable();

                if (!string.IsNullOrEmpty(filterModel.PharmacyId))
                    query = query.Where(o => o.PharmacyId == filterModel.PharmacyId);

                if (!string.IsNullOrEmpty(filterModel.Status))
                    query = query.Where(o => o.Status == filterModel.Status);

                if (filterModel.From.HasValue)
                    query = query.Where(o => o.CreatedAtDate >= filterModel.From.Value);

                if (filterModel.To.HasValue)
                    query = query.Where(o => o.CreatedAtDate <= filterModel.To.Value);


                if (filterModel.isSortByCreatedAt)
                {
                    query = filterModel.SortDescending
                        ? query.OrderByDescending(e => e.CreatedAt)
                        : query.OrderBy(e => e.CreatedAt);
                }
                else
                {
                    query = filterModel.SortDescending
                        ? query.OrderByDescending(e => e.TotalCents)
                        : query.OrderBy(e => e.TotalCents);
                }

                var totalCount = query.Count();
                var result = query
                    .Skip((filterModel.PageNumber - 1) * filterModel.PageSize)
                    .Take(filterModel.PageSize)
                    .ToList();

                return (result, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
