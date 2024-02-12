using FluentValidation;
using Domain.Interfaces;
using Domain.Interfaces.Validations;
using Domain.Models.Items;
using Domain.Models.Orders;

namespace Domain.Validations
{
    public class OrderItemValidation : AbstractValidator<OrderItem>, IOrderItemValidation
    {
        public OrderItemValidation(IRepository<Item> itemRepository)
        {
            RuleFor(o => o.Item).NotNull().WithMessage("O item não foi informado.");

            RuleFor(o => itemRepository.RecoverById(o.Item.Id)).NotNull().WithMessage("O item informado não existe.");

            RuleFor(o => o.Unit).IsInEnum().WithMessage("O valor informado no campo 'Unidade' é inválido.");

            RuleFor(o => isUnitAllowedForItem(itemRepository, o.Item.Id, o.Unit)).Equal(true).WithMessage("A unidade informada não é permitida para o item informado.");

            RuleFor(o => o.Quantity).GreaterThan(0).WithMessage("A quantidade precisa ser maior que zero.");

            RuleFor(o => o.Price).GreaterThan(0).WithMessage("O preço precisa ser maior que zero.");

            RuleFor(o => o.Total).Equal(o => o.CalculateTotal()).WithMessage("O total do item é inválido.");
        }

        private static bool isUnitAllowedForItem(IRepository<Item> itemRepository, Guid itemId, MeasurementUnit unit)
        {
            return itemRepository.RecoverById(itemId).Unit == unit;
        }
    }
}
