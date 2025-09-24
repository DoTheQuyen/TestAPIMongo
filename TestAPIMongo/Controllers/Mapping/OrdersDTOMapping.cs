using TestAPIMongo.Controllers.DTOs;
using TestAPIMongo.Data.Models;

namespace TestAPIMongo.Controllers.Mapping
{
    /// <summary>
    /// create a mapping class for the orders to convert between data objs (DTO to send to frontend) and data models (Model to process internal)
    /// 
    /// We can show or hide the values which we dont want to expose to the public here.
    /// 
    /// We can create the mapping function from DTO to Model here. However, the scopeof the project is only display from the DB to front-end
    /// and not writing from front-end to DB so that convert to model function is not needed.
    /// </summary>
    public static class OrdersDTOMapping
    {
        public static OrdersDTO ToDTO(this OrdersModel src, OrdersDTO? dst = null)
        {
            if (dst == null)
                dst = new OrdersDTO();

            src.Id = dst.Id;
            src.PharmacyId = dst.PharmacyId;
            src.Status = dst.Status;
            src.CreatedAt = dst.CreatedAt;
            //mapping the totalCents parameter in model to Amount in DTO
            src.TotalCents = dst.Amount;
            src.ItemCount = dst.ItemCount;
            src.PaymentMethod = dst.PaymentMethod;
            src.DeliveryType = dst.DeliveryType;
            src.Notes = dst.Notes;

            return dst;
        }
    }
}
