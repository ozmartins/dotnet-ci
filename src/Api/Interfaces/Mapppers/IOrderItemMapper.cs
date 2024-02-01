using Api.Dtos.Orders;
using Api.Infra;
using Api.Views.Orders;
using Domain.Models.Orders;

namespace Api.Interfaces.Mapppers
{
    public interface IOrderItemMapper
    {
        MapperResult<OrderItem> Map(OrderItemDto dto);

        List<MapperResult<OrderItem>> Map(List<OrderItemDto> dto);

        OrderItemView? Map(OrderItem? orderItem);

        List<OrderItemView> Map(List<OrderItem> orderItems);
    }
}
