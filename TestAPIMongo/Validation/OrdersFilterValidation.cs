using FluentValidation;
using TestAPIMongo.Data.Models;

namespace TestAPIMongo.Validation
{
    public class OrdersFilterValidation : AbstractValidator<OrdersFilterModel>
    {
        /// <summary>
        /// Validation class to validation data correction
        /// </summary>
        /// <typeparam name="OrdersFilterModel"></typeparam>
        /// <param name="model"></param>
        public OrdersFilterValidation()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(model => model.PageSize).NotEmpty().InclusiveBetween(20, 100).WithMessage("Page size should be between 20 to 100");
        }
    }
}
