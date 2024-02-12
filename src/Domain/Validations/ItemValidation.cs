using FluentValidation;
using FluentValidation.Results;
using Domain.Interfaces.Filters;
using Domain.Interfaces.Validations;
using Domain.Models.Items;
using Domain.Models.People;
using Domain.Interfaces;

namespace Domain.Validations
{
    public class ItemValidation : AbstractValidator<Item>, IItemValidation
    {
        private readonly IScheduleValidation _scheduleValidation;

        private readonly IItemScheduleValidation _itemScheduleValidation;

        public ItemValidation(IRepository<Item> itemRepository, IFilterBuilder<Item> itemFilterBuilder, IRepository<Person> personRepository, IScheduleValidation scheduleValidation, IItemScheduleValidation itemScheduleValidation)
        {
            _scheduleValidation = scheduleValidation;

            _itemScheduleValidation = itemScheduleValidation;

            RuleFor(x => x.SKU).NotEmpty().WithMessage("O SKU do item não foi informado.");

            RuleFor(x => skuAlreadyExists(itemRepository, itemFilterBuilder, x)).Equal(false).WithMessage("Já existe um item cadastrado com o SKU informado.");

            RuleFor(x => x.Supplier).NotNull().WithMessage("O fornecedor precisa ser informado");

            RuleFor(x => x.Supplier.SupplierOrCustomer).Equal(SupplierOrCustomer.Supplier).WithMessage("A pessoa informada não é um fornecedor.");

            RuleFor(x => personRepository.RecoverById(x.Supplier.Id)).NotNull().WithMessage("O fornecedor informado não existe.");

            RuleFor(x => x.Name).NotEmpty().WithMessage("O nome do item precisa ser informado.");

            RuleFor(x => x.Details).NotEmpty().WithMessage("Os detalhes do item precisam ser informados.");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("O preço do item precisa ser informado.");

            RuleFor(x => x.Unit).IsInEnum().WithMessage("A unidade de medida informada é inválida.");

            RuleFor(x => x.ProductOrService).IsInEnum().WithMessage("O valor do campo 'Produto ou Serviço' é inválido.");

            RuleFor(x => x.ProductInfo.ForRentOrSale).IsInEnum().WithMessage("O valor do campo 'Aluguel ou Venda' é inválido.");

            RuleFor(x => x.ProductInfo.AvailableQuantity).GreaterThanOrEqualTo(0).WithMessage("A quantidade disponível em estoque não pode ser negativa.");
        }

        public ValidationResult CustomValidate(Item item)
        {
            var result = Validate(item);

            if (!result.IsValid) return result;

            foreach (var schedule in item.Schedules)
            {
                result = _scheduleValidation.Validate(schedule);

                if (!result.IsValid) return result;
            }

            result = _itemScheduleValidation.Validate(item);

            if (!result.IsValid) return result;

            return result;
        }

        private static bool skuAlreadyExists(IRepository<Item> itemRepository, IFilterBuilder<Item> filterBuilder, Item item)
        {
            filterBuilder
                .Equal(x => x.SKU, item.SKU)
                .Equal(x => x.Supplier, item.Supplier)
                .Unequal(x => x.Id, item.Id);

            return itemRepository.Recover(filterBuilder).Count > 0;
        }
    }
}
