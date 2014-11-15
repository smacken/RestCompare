using FluentValidation;
using RestCompare.Api.Models;

namespace RestCompare.Api.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(1, 255);
        }
    }
}
