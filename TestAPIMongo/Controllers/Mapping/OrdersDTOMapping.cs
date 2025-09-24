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
        public static OrderResponseDto ToDTO(this OrderModel src, OrderResponseDto? dst = null)
        {
            if (dst == null)
                dst = new OrderResponseDto();

            dst.Id = src.Id;
            dst.PharmacyId = src.PharmacyId;
            dst.Status = src.Status;
            dst.CreatedAt = src.CreatedAt;
            //mapping the totalCents parameter in model to Amount in DTO
            dst.Amount = src.TotalCents;
            dst.ItemCount = src.ItemCount;
            dst.PaymentMethod = src.PaymentMethod;
            dst.DeliveryType = src.DeliveryType;
            dst.Notes = src.Notes;
            dst.NeedsReview = src.NeedsReview;

            return dst;
        }
    }
}
