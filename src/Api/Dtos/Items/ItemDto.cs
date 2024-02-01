using Domain.Models.Items;

namespace Api.Dtos.Items
{
    public class ItemDto
    {
        public Guid SupplierId { get; set; }
        public string SKU { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public object Photo { get; set; } = new object();
        public decimal Price { get; set; }
        public MeasurementUnit Unit { get; set; }
        public ProductOrService ProductOrService { get; set; }
        public RentOrSale ForRentOrSale { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
