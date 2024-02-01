namespace Api.Controllers.Constants
{
    public class CustomerConstant
    {
        public const string Tag = "Customer";

        public const string CreateSummary = "It creates a customer.";
        public const string UpdateSummary = "It updates a customer.";
        public const string DeleteSummary = "It removes a customer.";
        public const string GetByIdSummary = "It gets a specific customer.";
        public const string GetAllSummary = "It gets all the customers.";

        public const string CreateDescription = "This method would not be public, because a customer should be added internally when a user is created.";
        public const string DeleteDescription = "This method would not be public, because a customer should be removed internally when a user account was removed.";
        public const string UpdateDescription = "Your app will, probably, have a screen that will allow the user to change their own information. This is the method you will call for it.";
        public const string GetByIdDescription = "This is the method your app will use to show users their own information.";
        public const string GetAllDescription = "I can't see a scenario where this method should be used.";
    }
}
