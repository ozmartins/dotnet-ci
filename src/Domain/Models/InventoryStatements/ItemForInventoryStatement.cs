using Domain.Models.Items;

namespace Domain.Models.InventoryStatements
{
    public class ItemForInventoryStatement
    {
        public ItemForInventoryStatement()
        {
        }
        public ItemForInventoryStatement(Guid id, string sku, string name, MeasurementUnit unit)
        {
            Id = id;
            SKU = sku;
            Name = name;
            Unit = unit;
        }
        public Guid Id { get; private set; }
        public string SKU { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public MeasurementUnit Unit { get; private set; }
    }
}