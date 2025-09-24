using FluentValidation;
using Moq;
using NUnit.Framework;
using TestAPIMongo.Data.Interface;
using TestAPIMongo.Data.Models;
using TestAPIMongo.Services.Interface;
using TestAPIMongo.Services.Services;

namespace TestAPIMongo.Tests
{
    [TestFixture]
    public class OrdersServiceTests
    {
        private Mock<IOrders> _mockOrders;
        private Mock<IValidator<OrdersFilterModel>> _mockValidator;
        private Mock<ILogger<OrdersService>> _mockLogger;
        private IOrdersService _ordersService;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            _mockOrders = new Mock<IOrders>();
            _mockValidator = new Mock<IValidator<OrdersFilterModel>>();
            _mockLogger = new Mock<ILogger<OrdersService>>();

            var inMemorySettings = new Dictionary<string, string> {
                {"Review:DailyOrderThresholdCents", "50000"}
            };
            _configuration = new ConfigurationBuilder()
               .AddInMemoryCollection(inMemorySettings)
               .Build();

            _mockValidator
                .Setup(v => v.ValidateAsync(It.IsAny<OrdersFilterModel>(), default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _mockOrders
                .Setup(o => o.GetOrdersList(It.IsAny<OrdersFilterModel>()))
                .ReturnsAsync((new List<OrderModel> { new OrderModel() }, 1));

            _ordersService = new OrdersService(_mockOrders.Object, _mockValidator.Object, _mockLogger.Object, _configuration);
        }

        [Test]
        public async Task GetOrdersList_ValidFilter_ReturnsData()
        {
            var filter = new OrdersFilterModel { PageNumber = 1, PageSize = 10 };
            var (result, total) = await _ordersService.GetOrdersList(filter);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(total, Is.EqualTo(1));
        }
    }
}