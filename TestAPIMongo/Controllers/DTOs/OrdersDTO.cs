
namespace TestAPIMongo.Controllers.DTOs
{
    /// <summary>
    /// Define the data object which will be public the the front-end
    /// We can change the name or remove parameters here to not expose sensitive data to public
    /// Example; change the TotalCents to Amount, and mapping the value in mapping class
    /// </summary>
    public class OrdersDTO
    {
        //example to show nad hide params.
        //the _id is the object Id of database which does not need to be shown to front-end
        //public string? _id { get; set; }
        public string Id { get; set; }
        public string PharmacyId { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public long Amount { get; set; }

        public int ItemCount { get; set; }

        public string PaymentMethod { get; set; }

        public string? DeliveryType { get; set; }

        public string? Notes { get; set; }
    }
}
