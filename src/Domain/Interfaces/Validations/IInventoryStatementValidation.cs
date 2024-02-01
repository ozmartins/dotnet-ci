using FluentValidation;
using Domain.Models.InventoryStatements;

namespace Domain.Interfaces.Validations
{
    public interface IInventoryStatementValidation : IValidator<InventoryStatement>
    {
    }
}
