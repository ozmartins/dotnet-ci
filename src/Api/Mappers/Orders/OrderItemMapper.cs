using AutoMapper;
using Api.Dtos.Orders;
using Api.Infra;
using Api.Interfaces.Mapppers;
using Api.Views.Orders;
using Domain.Infra.Extensions;
using Domain.Interfaces;
using Domain.Models.Items;
using Domain.Models.Orders;

namespace Api.Mappers.Orders
{
    public class OrderItemMapper : BaseMapper<OrderItem>, IOrderItemMapper
    {
        private IRepository<Item> _itemRepository;

        private IMapper _autoMapper;

        public OrderItemMapper(IRepository<Item> itemRepository, IMapper autoMapper)
        {
            _itemRepository = itemRepository;
            _autoMapper = autoMapper;
        }

        public MapperResult<OrderItem> Map(OrderItemDto dto)
        {
            var result = MapToEntity(dto);

            foreach (var erro in result.Errors) AddError(erro);

            SetEntity(result.Entity);

            return GetResult();
        }

        public List<MapperResult<OrderItem>> Map(List<OrderItemDto> dtos)
        {
            var result = new List<MapperResult<OrderItem>>();

            foreach (var dto in dtos)
            {
                result.Add(MapToEntity(dto));
            }

            return result;
        }

        public OrderItemView? Map(OrderItem? orderItem)
        {
            return MapToView(orderItem);
        }

        public List<OrderItemView> Map(List<OrderItem> orderItems)
        {
            var items = new List<OrderItemView>();

            foreach (var orderItem in orderItems)
            {
                var item = MapToView(orderItem);
                if (item != null)
                    items.Add(item);
            }

            return items;
        }

        private MapperResult<OrderItem> MapToEntity(OrderItemDto dto)
        {
            var result = new MapperResult<OrderItem>();

            var item = _itemRepository.RecoverById(dto.ItemId).IfNull(() => { result.Errors.Add("O item informado não existe."); });

            if (!result.Success) return result;

            result.DefineEntity(new OrderItem(_autoMapper.Map<ItemForOrder>(item), item!.Unit, dto.Quantity, item.Price));

            return result;
        }

        private OrderItemView? MapToView(OrderItem? orderItem)
        {
            if (orderItem == null) return null;

            var orderItemView = new OrderItemView()
            {
                Id = orderItem.Id,
                Version = orderItem.Version,
                ItemId = orderItem.Item.Id,
                Price = orderItem.Price,
                Unit = orderItem.Unit,
                Quantity = orderItem.Quantity,
                Total = orderItem.Total
            };

            return orderItemView;
        }
    }
}
