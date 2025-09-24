namespace TestAPIMongo.Data.Models
{
    public class DatabaseSetting
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string OrdersCollectionName { get; set; } = null!;
    }
}
