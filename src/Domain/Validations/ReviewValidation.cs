using FluentValidation;
using Domain.Interfaces;
using Domain.Interfaces.Filters;
using Domain.Interfaces.Validations;
using Domain.Models.Review;

namespace Domain.Validations
{
    public class ReviewValidation : AbstractValidator<Review>, IReviewValidation
    {
        public ReviewValidation(IRepository<Review> reviewRepository,
                                IFilterBuilder<Review> reviewFilterBuilder)
        {
            RuleFor(p => p.DateTime).GreaterThan(DateTime.MinValue).WithMessage("A data da avaliação não foi informada.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("A descrição da avaliação não foi informada.");
            RuleFor(p => p.Stars).InclusiveBetween(1, 5).WithMessage("As quantidades de estrelas precisam estar entre 1 e 5.");
            RuleFor(p => reviewAlreadyExistsForOrderItem(reviewRepository, reviewFilterBuilder, p)).Equal(false).WithMessage("Já existe uma avaliação para este item do pedido.");

        }

        private bool reviewAlreadyExistsForOrderItem(IRepository<Review> reviewRepository, IFilterBuilder<Review> reviewFilterBuilder, Review review)
        {
            reviewFilterBuilder.Equal(x => x.OrderItem, review.OrderItem)
                               .Unequal(x => x.Id, review.Id);
            var teste = reviewRepository.Recover(reviewFilterBuilder);

            return reviewRepository.Recover(reviewFilterBuilder).Count > 0;
        }
    }
}
