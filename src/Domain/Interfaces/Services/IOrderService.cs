using Domain.Misc;
using Domain.Models.Orders;

namespace Domain.Interfaces.Services
{
    public interface IOrderService : IService<Order>
    {
        public ServiceResult<Order> Create(Order order);

        public ServiceResult<Order> Update(Guid id, Order order);

        public ServiceResult<Order> AddOrderItem(Guid orderId, OrderItem orderItem);

        public ServiceResult<Order> ReplaceOrderItem(Guid orderId, Guid orderItemId, OrderItem orderItem);

        public ServiceResult<Order> RemoveOrderItem(Guid orderId, Guid orderItemId);
    }
}
