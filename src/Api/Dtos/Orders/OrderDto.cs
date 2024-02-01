using Api.Dtos.Addresses;

namespace Api.Dtos.Orders
{
    public class OrderDto
    {
        public Guid SupplierId { get; set; }
        public Guid CustomerId { get; set; }
        public AddressDto ShippingAddress { get; set; } = new AddressDto();
        public decimal Freight { get; set; }
        public Guid PaymentPlanId { get; set; }
        public int Installments { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime PartyDate { get; set; }
    }
}
