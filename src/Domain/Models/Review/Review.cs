namespace Domain.Models.Review
{
    public class Review : Entity
    {
        public Review() : base() { }
        public Review(DateTime dateTime, int stars, string description, OrderItemForReview orderItem)
        {
            DateTime = dateTime;
            Stars = stars;
            Description = description;
            OrderItem = orderItem;
        }
        public DateTime DateTime { get; private set; }
        public int Stars { get; private set; } = 0;
        public string Description { get; private set; } = string.Empty;
        public OrderItemForReview OrderItem { get; private set; } = new OrderItemForReview();
    }
}
