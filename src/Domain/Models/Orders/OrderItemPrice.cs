namespace Domain.Models.Orders
{
    public class OrderItemPrice
    {
        public OrderItemPrice() { }
        public OrderItemPrice(Guid orderItemId, decimal price)
        {
            OrderItemId = orderItemId;
            Price = price;
        }
        public Guid OrderItemId { get; private set; }
        public decimal Price { get; private set; }
    }
}
