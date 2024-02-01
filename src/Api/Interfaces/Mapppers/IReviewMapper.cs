using Api.Dtos.Reviews;
using Api.Infra;
using Api.Views.Reviews;
using Domain.Models.Review;

namespace Api.Interfaces.Mapppers
{
    public interface IReviewMapper
    {
        MapperResult<Review> Map(ReviewDto dto);
        ReviewView? Map(Review? review);
        List<ReviewView> Map(List<Review> reviews);
    }
}
