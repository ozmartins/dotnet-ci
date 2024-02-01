using AutoMapper;
using Api.Dtos.Items;
using Api.Infra;
using Api.Interfaces.Mappers;
using Api.Views.Items;
using Domain.Infra.Extensions;
using Domain.Models.Items;
using Domain.Models.People;
using Domain.Interfaces;

namespace Api.Mappers.Items
{
    public class ItemMapper : BaseMapper<Item>, IItemMapper
    {
        private IMapper _autoMapper;

        private IRepository<Person> _personRepository;

        public ItemMapper(IMapper autoMapper, IRepository<Person> personRepository)
        {
            _autoMapper = autoMapper;
            _personRepository = personRepository;
        }

        public MapperResult<Item> Map(ItemDto dto)
        {
            var supplier = _personRepository.RecoverById(dto.SupplierId).IfNull(() => { AddError("O fornecedor informado não existe."); });

            if (!SuccessResult()) return GetResult();

            var item = new Item
            (
                new PersonForItem(supplier!.Id, supplier.Name, supplier.SupplierOrCustomer),
                dto.SKU,
                dto.Name,
                dto.Details,
                dto.Photo,
                dto.Price,
                dto.Unit,
                dto.ProductOrService,
                new Product(dto.AvailableQuantity, dto.ForRentOrSale),
                new List<Schedule>()
            );

            SetEntity(item);

            return GetResult();
        }

        public ItemView? Map(Item? item)
        {
            return MapToView(item);
        }

        public List<ItemView> Map(List<Item> items)
        {
            var result = new List<ItemView>();

            foreach (var item in items)
            {
                var mapped_item = MapToView(item);
                if (mapped_item != null)
                    result.Add(mapped_item);
            }

            return result;
        }

        private ItemView? MapToView(Item? item)
        {
            if (item == null) return null;

            var itemView = new ItemView()
            {
                Id = item.Id,
                Version = item.Version,
                SupplierId = item.Supplier.Id,
                SKU = item.SKU,
                Name = item.Name,
                Details = item.Details,
                Photo = item.Photo,
                Price = item.Price,
                Unit = item.Unit,
                ProductOrService = item.ProductOrService,
                AvailableQuantity = item.ProductInfo.AvailableQuantity,
                ForRentOrSale = item.ProductInfo.ForRentOrSale
            };

            return itemView;
        }

    }
}
