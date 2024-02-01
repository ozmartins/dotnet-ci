namespace Api.Controllers.Constants
{
    public class InventoryStatementConstant
    {
        public const string Tag = "Inventory Statement";

        public const string CreateSummary = "It creates an inventory statement entry";
        public const string DeleteSummary = "It removes an inventory statement entry";
        public const string GetByIdSummary = "It gets a specific inventory statement entry";
        public const string GetAllSummary = "It gets all the inventory statement entries";

        public const string CreateDescription = "This method shouldn't be public, because an inventory statement entry must be created when an order is shipped.";
        public const string DeleteDescription = "This method shouldn't be public, because an inventory statement entry must be removed when an order shipping is canceled.";
        public const string GetByIdDescription = "I can't see a scenario where this method will be used.";
        public const string GetAllDescription = "If your app wants to show to suppliers their inventory statements, use this method.";
    }
}
