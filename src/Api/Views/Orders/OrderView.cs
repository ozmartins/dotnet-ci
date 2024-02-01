using Domain.Models.Orders;

namespace Api.Views.Orders
{
    public class OrderView : View
    {
        public DateTime DateTime { get; set; }
        public Guid SupplierId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ShippingAddressId { get; set; }
        public decimal Freight { get; set; }
        public decimal PaymentPlanFee { get; set; }
        public decimal ItemsTotal { get; set; }
        public decimal OrderTotal { get; set; }
        public Guid PaymentPlanId { get; set; }
        public string Notes { get; set; } = string.Empty;
        public OrderStatus Status { get; set; }
        public DateTime PartyDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
