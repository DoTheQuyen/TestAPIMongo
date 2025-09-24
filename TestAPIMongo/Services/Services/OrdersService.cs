using FluentValidation;
using TestAPIMongo.Data.Interface;
using TestAPIMongo.Data.Models;
using TestAPIMongo.Services.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestAPIMongo.Services.Services
{
    /// <summary>
    /// Service layer is only use to receive data from controller to check for:
    /// authorization
    /// validation
    /// process business rule
    /// Do not process data access in this layer. Pass down the data to data layer to process data connection
    /// </summary>
    public class OrdersService : IOrdersService
    {

        private readonly IOrders _order;
        private readonly IValidator<OrdersFilterModel> _validator;
        private readonly ILogger<OrdersService> _logger;

        public OrdersService(IOrders order, IValidator<OrdersFilterModel> validator, ILogger<OrdersService> logger)
        {
            _order = order;
            _validator = validator;
            _logger = logger;
        }

        /// <summary>
        /// GetOrdersList function to receive filter data from controller and process logic        /// 
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<(List<OrdersModel>, long)> GetOrdersList(OrdersFilterModel filterModel)
        {
            try
            {
                //Use FluentValidation to validate rules of input object before process
                var validate = await _validator.ValidateAsync(filterModel);
                if (!validate.IsValid)
                {
                    var errors = string.Join("; ", validate.Errors.Select(e => e.ErrorMessage));
                    _logger.LogInformation("Validation failed:={errors}", errors);
                    throw new ArgumentException($"Validation failed: {errors}");
                }

                //Authorization here


                //Process business rule here

                var result = await _order.GetOrdersList(filterModel);
                _logger.LogInformation("Returned {Count} orders", result.Item1.Count);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error in GetOrdersList:={errors}", ex);
                throw new Exception("Error in GetOrdersList", ex);
            }
           
        }

    }
}
