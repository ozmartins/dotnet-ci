using AutoMapper;
using Api.Dtos.InventoryStatements;
using Api.Infra;
using Api.Interfaces.Mappers;
using Api.Views.InventoryStatements;
using Domain.Infra.Extensions;
using Domain.Models.InventoryStatements;
using Domain.Models.Items;
using Domain.Interfaces;

namespace Api.Mappers.InventoryStatements
{
    public class InventoryStatementMapper : BaseMapper<InventoryStatement>, IInventoryStatementMapper
    {
        private readonly IRepository<Item> _itemRepository;

        private readonly IMapper _autoMapper;

        public InventoryStatementMapper(IRepository<Item> itemRepository, IMapper autoMapper)
        {
            _itemRepository = itemRepository;
            _autoMapper = autoMapper;
        }

        public MapperResult<InventoryStatement> Map(InventoryStatementDto dto)
        {
            var product = _itemRepository.RecoverById(dto.ProductId).IfNull(() => { AddError("O item informado não existe."); });

            if (!SuccessResult()) return GetResult();

            SetEntity(new InventoryStatement(
                new ItemForInventoryStatement(
                    product?.Id ?? Guid.Empty,
                    product?.SKU ?? string.Empty,
                    product?.Name ?? string.Empty, product?.Unit ?? MeasurementUnit.Piece),
                dto.Quantity,
                dto.InOrOut,
                dto.DataTime));

            return GetResult();
        }

        public InventoryStatementView? Map(InventoryStatement? inventoryStatement)
        {
            return MapToView(inventoryStatement);
        }

        public List<InventoryStatementView> Map(List<InventoryStatement> inventoryStatements)
        {
            var result = new List<InventoryStatementView>();

            foreach (var inventoryStatement in inventoryStatements)
            {
                var mapped = MapToView(inventoryStatement);
                if (mapped != null)
                    result.Add(mapped);
            }

            return result;
        }

        private static InventoryStatementView? MapToView(InventoryStatement? inventoryStatement)
        {
            if (inventoryStatement == null) return null;

            var inventoryStatementView = new InventoryStatementView()
            {
                Id = inventoryStatement.Id,
                Version = inventoryStatement.Version,
                DataTime = inventoryStatement.DateTime,
                InOrOut = inventoryStatement.InOrOut,
                ProductId = inventoryStatement.Product.Id,
                Quantity = inventoryStatement.Quantity
            };

            return inventoryStatementView;
        }

    }
}
