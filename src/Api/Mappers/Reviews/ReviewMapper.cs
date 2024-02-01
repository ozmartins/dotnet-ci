using AutoMapper;
using Api.Dtos.Reviews;
using Api.Infra;
using Api.Interfaces.Mapppers;
using Api.Views.Reviews;
using Domain.Infra.Extensions;
using Domain.Models.Orders;
using Domain.Models.Review;
using Domain.Interfaces;
using MongoDB.Driver;

namespace Api.Mappers.Reviews
{
    public class ReviewMapper : BaseMapper<Review>, IReviewMapper
    {
        private IRepository<Order> _orderRepository;
        private readonly IMapper _autoMapper;
        public ReviewMapper(IRepository<Order> orderRepository, IMapper autoMapper)
        {
            _orderRepository = orderRepository;
            _autoMapper = autoMapper;
        }

        public MapperResult<Review> Map(ReviewDto dto)
        {
            var order = GetOrder(dto.OrderId);
            var orderItem = getOrderItem(order, dto.OrderItemId);

            if (!SuccessResult()) return GetResult();

            SetEntity(new Review(
                dto.Date,
                dto.Stars,
                dto.Description,
                new OrderItemForReview(
                    order?.Id ?? Guid.Empty,
                    orderItem?.Id ?? Guid.Empty,
                    new ItemForOrderItemForReview(orderItem?.Item.Id ?? Guid.Empty,
                    orderItem?.Item.Name ?? string.Empty))));

            return GetResult();
        }

        public ReviewView? Map(Review? review)
        {
            return MapToView(review);
        }

        public List<ReviewView> Map(List<Review> reviews)
        {
            var reviewsView = new List<ReviewView>();

            foreach (Review review in reviews)
            {
                var item = MapToView(review);
                if (item != null)
                    reviewsView.Add(item);
            }

            return reviewsView;
        }

        private ReviewView? MapToView(Review? review)
        {
            if (review == null) return null;
            return _autoMapper.Map<ReviewView>(review);
        }

        private OrderItem? getOrderItem(Order? order, Guid orderItemId)
        {
            if (order == null) return null;
            return order.Items.FirstOrDefault(x => x.Id == orderItemId).IfNull(() => AddError("O item do pedido informado na avaliação não existe."));
        }

        private Order? GetOrder(Guid orderId)
        {
            return _orderRepository.RecoverById(orderId).IfNull(() => AddError("O pedido informado na avaliação não existe."));
        }
    }
}
