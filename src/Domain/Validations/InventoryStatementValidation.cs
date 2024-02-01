using FluentValidation;
using Domain.Interfaces.Validations;
using Domain.Models.InventoryStatements;
using Domain.Models.Items;
using Domain.Interfaces;

namespace Domain.Validations
{
    public class InventoryStatementValidation : AbstractValidator<InventoryStatement>, IInventoryStatementValidation
    {
        public InventoryStatementValidation(IRepository<Item> itemRepository)
        {
            RuleFor(x => x.Product).NotNull().WithMessage("O item não foi informado.");

            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("A quantidade precisa ser maior que zero.");

            RuleFor(x => x.InOrOut).IsInEnum().WithMessage("O valor do campo 'Entrada ou Saída' é inválido.");

            RuleFor(x => x.DateTime).GreaterThan(DateTime.MinValue).WithMessage("A data/hora do lançamento não foi informada.");

            RuleFor(x => itemRepository.RecoverById(x.Product.Id)).NotNull().WithMessage("O item informado não existe.");
        }
    }
}
