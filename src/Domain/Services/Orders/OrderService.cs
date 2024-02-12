using Domain.Misc;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.Interfaces.Validations;
using Domain.Models.Orders;

namespace Domain.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly BasicService<Order> _basicService;

        private readonly IRepository<Order> _repository;

        private readonly IOrderValidation _orderValidation;

        private readonly IOrderItemValidation _orderItemValidation;

        public OrderService(IRepository<Order> repository, IOrderValidation orderValidation, IOrderItemValidation orderItemValidation)
        {
            _orderValidation = orderValidation;
            _orderItemValidation = orderItemValidation;
            _basicService = new BasicService<Order>(repository, orderValidation);
            _repository = repository;
        }

        public ServiceResult<Order> Create(Order order)
        {
            order.SetDefaultValuesForNewOrder();

            order.TotalizeOrder();

            var result = _orderValidation.CustomValidate(order);

            if (!result.IsValid)
                return ServiceResult<Order>.FailureResult(result);

            _repository.Create(order);

            return ServiceResult<Order>.SuccessResult(order);
        }

        public ServiceResult<Order> Update(Guid id, Order order)
        {
            var currentOrder = Get(id);

            if (currentOrder == null)
                return ServiceResult<Order>.FailureResult("Não foi possível localizar o pedido informado.");

            currentOrder.CopyHeaderData(order);

            currentOrder.Items.RemoveAll(x => itemRemoved(x.Item.Id, currentOrder, order));

            currentOrder.Items.AddRange(itemsAdded(currentOrder, order));

            currentOrder.CopyItemsData(order);

            currentOrder.TotalizeOrder();

            var result = _orderValidation.CustomValidate(currentOrder);

            if (!result.IsValid)
                return ServiceResult<Order>.FailureResult(result);

            _repository.Update(id, currentOrder);

            return ServiceResult<Order>.SuccessResult(currentOrder);
        }

        public ServiceResult<Order> Delete(Guid id)
        {
            return _basicService.Delete(id);
        }

        public Order Get(Guid id)
        {
            return _basicService.Get(id);
        }

        public List<Order> Get()
        {
            return _basicService.Get();
        }

        public ServiceResult<Order> AddOrderItem(Guid orderId, OrderItem orderItem)
        {
            orderItem.TotalizeOrderItem();

            var order = Get(orderId);

            if (order == null)
                return ServiceResult<Order>.FailureResult("Não foi possível localizar o pedido informado.");

            var result = _orderItemValidation.Validate(orderItem);

            if (!result.IsValid)
                return ServiceResult<Order>.FailureResult(result);

            order.Items.Add(orderItem);

            order.TotalizeOrder();

            result = _orderValidation.CustomValidate(order);

            if (!result.IsValid)
                return ServiceResult<Order>.FailureResult(result);

            _repository.Update(orderId, order);

            return ServiceResult<Order>.SuccessResult(order);
        }

        public ServiceResult<Order> ReplaceOrderItem(Guid orderId, Guid orderItemId, OrderItem orderItem)
        {
            orderItem.TotalizeOrderItem();

            var order = Get(orderId);

            if (order == null)
                return ServiceResult<Order>.FailureResult("Não foi possível localizar o pedido informado.");

            var result = _orderItemValidation.Validate(orderItem);

            if (!result.IsValid)
                return ServiceResult<Order>.FailureResult(result);

            order.ReplaceItem(orderItemId, orderItem);

            order.TotalizeOrder();

            result = _orderValidation.CustomValidate(order);

            if (!result.IsValid)
                return ServiceResult<Order>.FailureResult(result);

            _repository.Update(orderId, order);

            return ServiceResult<Order>.SuccessResult(order);
        }

        public ServiceResult<Order> RemoveOrderItem(Guid orderId, Guid orderItemId)
        {
            var order = Get(orderId);

            if (order == null)
                return ServiceResult<Order>.FailureResult("Não foi possível localizar o pedido informado.");

            order.RemoveItem(orderItemId);

            order.TotalizeOrder();

            var result = _orderValidation.CustomValidate(order);

            if (!result.IsValid)
                return ServiceResult<Order>.FailureResult(result);

            _repository.Update(orderId, order);

            return ServiceResult<Order>.SuccessResult(order);
        }

        private static List<OrderItem> itemsAdded(Order currentOrder, Order newOrder)
        {
            var result = new List<OrderItem>();

            foreach (var item in newOrder.Items)
            {
                if (!currentOrder.Items.Exists(x => x.Item.Id == item.Item.Id))
                {
                    result.Add(item);
                }
            }

            return result;
        }
        private static bool itemRemoved(Guid itemId, Order currentOrder, Order newOrder)
        {
            return currentOrder.Items.Exists(x => x.Item.Id == itemId) && !newOrder.Items.Exists(x => x.Item.Id == itemId);
        }
    }
}
