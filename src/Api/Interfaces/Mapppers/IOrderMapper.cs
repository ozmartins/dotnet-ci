using Api.Dtos.Orders;
using Api.Infra;
using Api.Views.Orders;
using Domain.Models.Orders;

namespace Api.Interfaces.Mapppers
{
    public interface IOrderMapper
    {
        MapperResult<Order> Map(OrderDto dto);

        OrderView? Map(Order? order);

        List<OrderView> Map(List<Order> orders);
    }
}
