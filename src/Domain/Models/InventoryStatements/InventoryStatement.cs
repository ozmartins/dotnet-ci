namespace Domain.Models.InventoryStatements
{
    public class InventoryStatement : Entity
    {
        public InventoryStatement() : base() { }
        public InventoryStatement(ItemForInventoryStatement product, decimal quantity, InOrOut inOrOut, DateTime dateTime)
        {
            Product = product;
            Quantity = quantity;
            InOrOut = inOrOut;
            DateTime = dateTime;
        }
        public ItemForInventoryStatement Product { get; private set; } = new ItemForInventoryStatement();
        public decimal Quantity { get; private set; }
        public InOrOut InOrOut { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}
