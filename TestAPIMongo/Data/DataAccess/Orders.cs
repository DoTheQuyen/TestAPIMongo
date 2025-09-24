using Microsoft.Extensions.Options;
using MongoDB.Driver;
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
        private readonly IMongoCollection<OrdersModel> _ordersCollection;

        public Orders(IOptions<DatabaseSetting> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

            _ordersCollection = mongoDatabase.GetCollection<OrdersModel>(dbSettings.Value.OrdersCollectionName);
        }

        public Orders(IMongoCollection<OrdersModel> ordersCollection)
        {
            _ordersCollection = ordersCollection;
        }

        /// <summary>
        /// GetOrdersList to filter the list of orders base on filter model
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<(List<OrdersModel>, long)> GetOrdersList(OrdersFilterModel filterModel)
        {
            try
            {
                var builder = Builders<OrdersModel>.Filter;
                var filter = builder.Empty;

                if (!string.IsNullOrEmpty(filterModel.PharmacyId))
                    filter &= builder.Eq(e => e.PharmacyId, filterModel.PharmacyId);

                if (!string.IsNullOrEmpty(filterModel.Status))
                    filter &= builder.Eq(e => e.Status, filterModel.Status);

                if (filterModel.From.HasValue)
                    filter &= builder.Gte(e => e.CreatedAt, filterModel.From.Value);

                if (filterModel.To.HasValue)
                    filter &= builder.Lte(e => e.CreatedAt, filterModel.To.Value);

                var totalCount = await _ordersCollection.CountDocumentsAsync(filter);

                var query = _ordersCollection.Find(filter);

                if (filterModel.isSortByCreatedAt)
                {
                    query = filterModel.SortDescending
                        ? query.SortByDescending(e => e.CreatedAt)
                        : query.SortBy(e => e.CreatedAt);
                }
                else
                {
                    query = filterModel.SortDescending
                        ? query.SortByDescending(e => e.TotalCents)
                        : query.SortBy(e => e.TotalCents);
                }
                    

                var result = await query
                            .Skip((filterModel.PageNumber - 1) * filterModel.PageSize)
                            .Limit(filterModel.PageSize)
                            .ToListAsync();

                return (result, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
