namespace Api.Controllers.Constants
{
    public class OrderItemConstant
    {
        public const string Tag = "Order";

        public const string CreateSummary = "Add an item to an order.";
        public const string UpdateSummary = "Update a item's order.";
        public const string DeleteSummary = "Remove a item from an order.";
        public const string GetByIdSummary = "Get a specific item from a customer's order.";
        public const string GetAllSummary = "Get all the items from a specific customer's order.";

        public const string CreateDescription = "Honestly, this metohod shouldn't exist. Because the cities must be supplied by the internal team of the system.";
        public const string DeleteDescription = "Please, don't call this method. I'm begging you.";
        public const string UpdateDescription = "Please, don't call this method. I'm begging you.";
        public const string GetByIdDescription = "If you want to see a specific item from an order, call this method.";
        public const string GetAllDescription = "If you want to see all the items from an order, call this method.";
    }
}
