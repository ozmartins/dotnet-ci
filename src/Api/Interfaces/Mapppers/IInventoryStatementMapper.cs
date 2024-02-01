using Api.Dtos.InventoryStatements;
using Api.Infra;
using Api.Views.InventoryStatements;
using Domain.Models.InventoryStatements;

namespace Api.Interfaces.Mappers
{
    public interface IInventoryStatementMapper
    {
        MapperResult<InventoryStatement> Map(InventoryStatementDto dto);

        InventoryStatementView? Map(InventoryStatement? entity);

        List<InventoryStatementView> Map(List<InventoryStatement> entities);
    }
}
