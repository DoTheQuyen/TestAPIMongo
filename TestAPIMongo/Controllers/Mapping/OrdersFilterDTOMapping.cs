using TestAPIMongo.Data.Models;
using TestAPIMongo.Controllers.DTOs;

namespace TestAPIMongo.Controllers.Mapping
{
    public static class OrdersFilterDTOMapping
    {
        public static OrdersFilterModel ToModel(this OrdersFilterDTO src, OrdersFilterModel? dst = null)
        {
            if (dst == null)
                dst = new OrdersFilterModel();

            dst.PharmacyId = src.PharmacyId;
            dst.Status = src.Status;
            dst.From = src.From;
            dst.To = src.To;
            dst.isSortByCreatedAt = src.isSortByCreatedAt;
            dst.SortDescending = src.SortDescending;
            dst.PageNumber = src.PageNumber;
            dst.PageSize = src.PageSize;

            return dst;
        }
    }
}
