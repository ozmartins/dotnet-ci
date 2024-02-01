namespace Api.Controllers.Constants
{
    public class OrderConstant
    {
        public const string Tag = "Order";

        public const string CreateSummary = "Create an order to a customer.";
        public const string UpdateSummary = "Update a customers's order.";
        public const string DeleteSummary = "Remove a customer's order.";
        public const string GetByIdSummary = "Get a specific customer's order.";
        public const string GetAllSummary = "Get all the order from a specific customer.";

        public const string CreateDescription = "Honestly, this metohod shouldn't exist. Because the cities must be supplied by the internal team of the system.";
        public const string DeleteDescription = "Please, don't call this method. I'm begging you.";
        public const string UpdateDescription = "Please, don't call this method. I'm begging you.";
        public const string GetByIdDescription = "If you want to see details of the customer or supplier city, use this method.";
        public const string GetAllDescription = "If you want to create a search field that allows the user to find their own city, use this method.";
    }
}
