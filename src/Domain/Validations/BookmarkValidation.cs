using FluentValidation;
using Domain.Interfaces.Filters;
using Domain.Interfaces.Validations;
using Domain.Models.Bookmark;
using Domain.Models.Items;
using Domain.Models.People;
using Domain.Interfaces;

namespace Domain.Validations
{
    public class BookmarkValidation : AbstractValidator<Bookmark>, IBookmarkValidation
    {
        public BookmarkValidation(IRepository<Bookmark> bookmarkRepository, IRepository<Person> personRepository, IRepository<Item> itemRepository, IFilterBuilder<Bookmark> filterBuilder)
        {
            RuleFor(p => p.Customer).NotNull().WithMessage("O cliente não foi informado.");

            RuleFor(p => p.DateTime).GreaterThan(DateTime.MinValue).WithMessage("A data/hora da mensagem não foi informada.");

            RuleFor(x => personRepository.RecoverById(x.Customer.Id)).NotNull().WithMessage("O cliente informado não existe.");

            RuleFor(x => itemRepository.RecoverById(x.Item.Id)).NotNull().WithMessage("O item informado não existe.");

            RuleFor(p => bookmarkAlreadyExists(bookmarkRepository, filterBuilder, p)).Equal(false).WithMessage("Já existe um bookmark com mesmo cliente e item.");
        }

        private bool bookmarkAlreadyExists(IRepository<Bookmark> bookmarkRepository, IFilterBuilder<Bookmark> filterBuilder, Bookmark boomark)
        {
            filterBuilder
                .Equal(x => x.Item, boomark.Item)
                .Equal(x => x.Customer, boomark.Customer)
                .Unequal(x => x.Id, boomark.Id);

            return bookmarkRepository.Recover(filterBuilder).Count > 0;
        }
    }
}
