using Domain.Models.InventoryStatements;

namespace Api.Views.InventoryStatements
{
    public class InventoryStatementView : View
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public InOrOut InOrOut { get; set; }
        public DateTime DataTime { get; set; }
    }
}
