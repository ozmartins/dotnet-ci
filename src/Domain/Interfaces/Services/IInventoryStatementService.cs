using Domain.Misc;
using Domain.Models.InventoryStatements;

namespace Domain.Interfaces.Services
{
    public interface IInventoryStatementService : IService<InventoryStatement>
    {
        public ServiceResult<InventoryStatement> Create(InventoryStatement inventoryStatement);
    }
}
