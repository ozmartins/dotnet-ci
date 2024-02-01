namespace Domain.Models.Reservation
{
    public class OrderItemForReservation
    {
        public OrderItemForReservation() { }
        public OrderItemForReservation(Guid orderId, Guid orderItemId)
        {
            OrderId = orderId;
            OrderItemId = orderItemId;
        }
        public Guid OrderId { get; private set; }
        public Guid OrderItemId { get; private set; }
    }
}
