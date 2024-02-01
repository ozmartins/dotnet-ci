using Domain.Models.Items;

namespace Api.Views.Orders
{
    public class OrderItemView : View
    {
        public Guid ItemId { get; set; }
        public MeasurementUnit Unit { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
