using FluentValidation;
using Domain.Models.Review;

namespace Domain.Interfaces.Validations
{
    public interface IReviewValidation : IValidator<Review>
    {
    }
}
