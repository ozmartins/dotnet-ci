using FluentValidation;
using Domain.Models.Bookmark;

namespace Domain.Interfaces.Validations
{
    public interface IBookmarkValidation : IValidator<Bookmark>
    {
    }
}
