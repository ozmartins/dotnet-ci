using Domain.Models.Items;

namespace Api.Views.Items
{
    public class ItemView : View
    {
        public Guid SupplierId { get; set; }
        public string SKU { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public object? Photo { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public MeasurementUnit Unit { get; set; }
        public ProductOrService ProductOrService { get; set; }
        public decimal AvailableQuantity { get; set; }
        public RentOrSale ForRentOrSale { get; set; }
    }
}
