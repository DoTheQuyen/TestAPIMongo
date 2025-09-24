using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestAPIMongo.Data.Models
{
    public class OrderModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }


        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("pharmacyId")]
        public string PharmacyId { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("createdAt")]
        public string CreatedAt { get; set; }

        [BsonElement("totalCents")]
        public long TotalCents { get; set; }

        [BsonElement("itemCount")]
        public int ItemCount { get; set; }

        [BsonElement("paymentMethod")]
        public string PaymentMethod { get; set; }

        [BsonElement("deliveryType")]
        public string? DeliveryType { get; set; }

        [BsonElement("notes")]
        public string? Notes { get; set; }

        
        public bool? NeedsReview { get; set; }

        [BsonIgnore]
        public DateTime? CreatedAtDate =>
            DateTime.TryParse(CreatedAt, out var dt) ? dt : null;

    }
}
