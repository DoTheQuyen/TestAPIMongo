
namespace TestAPIMongo.Data.Models
{
    public class OrdersFilterModel
    {
        public string? PharmacyId { get; set; }

        public string? Status { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public bool isSortByCreatedAt { get; set; } = true;

        public bool SortDescending { get; set; } = true;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
