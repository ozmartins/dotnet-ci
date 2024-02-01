namespace Domain.Models.Review
{
    public class OrderItemForReview
    {
        public OrderItemForReview() { }
        public OrderItemForReview(Guid id, Guid orderId, ItemForOrderItemForReview item)
        {
            Id = id;
            OrderId = orderId;
            Item = item;
        }
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public ItemForOrderItemForReview Item { get; private set; } = new ItemForOrderItemForReview();
    }
}
