using Microsoft.AspNetCore.Mvc;
using TestAPIMongo.Controllers.DTOs;
using TestAPIMongo.Controllers.Mapping;
using TestAPIMongo.Services.Interface;

namespace TestAPIMongo.Controllers
{
    /// <summary>
    /// Controller wont do anything, only play a role as the API gateway to listen for the call event, 
    /// authentication and pass down to service to process
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        /// <summary>
        /// The API to filter the orders list
        /// Use the filter DTO instead of get a list a separate params so that the code is clean, easy to remember and wont missing any params
        /// HTTPGet can only read the params in url, not the object so that use the HTTPPost for the API need to receive the object in body
        /// </summary>
        /// <param name="ordersFilter"></param>
        /// <returns></returns>
        [HttpPost("get-orders-list")]
        public async Task<IActionResult> GetOrdersList([FromBody] OrdersFilterDto ordersFilter)
        {          
            //Convert the DTO to model and send to service to process result
            var (orders, totalCount) = await _ordersService.GetOrdersList(ordersFilter.ToModel());

            var response = new
            {
                totalCount,
                ordersFilter.PageSize,
                currentPage = ordersFilter.PageNumber,
                totalPages = (int)Math.Ceiling((double)totalCount / ordersFilter.PageSize),
                //Convert from model to DTO and return to front-end
                data = orders.Select(x => x.ToDTO()).ToList()
            };

            return Ok(response);
        }
    }
}
