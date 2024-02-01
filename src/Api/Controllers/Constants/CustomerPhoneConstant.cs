namespace Api.Controllers.Constants
{
    public class CustomerPhoneConstant
    {
        public const string Tag = "Customer";

        public const string CreateSummary = "It adds a phone to a customer.";
        public const string UpdateSummary = "It updates a customer's phone.";
        public const string DeleteSummary = "It removes a phone from a customer.";
        public const string GetByIdSummary = "It gets a specific phone from a specic customer.";
        public const string GetAllSummary = "It gets all phones from a specific customer.";

        public const string CreateDescription = "Your app will have a screen where users will inform their own phone numbers. This screen will use this method to persist those numbers.";
        public const string UpdateDescription = "Users can make mistakes when typing phone numbers. Use this method to fix them.";
        public const string DeleteDescription = "Eventually, the user wants to remove a specific phone number and your app will call this method for it.";
        public const string GetByIdDescription = "This is the method your app will use to show one specifi customer phone. I can't see a scenario where this can be useful.";
        public const string GetAllDescription = "This is the method your app will use to show all customers phones. It can be used by the user themself or by suppliers.";
    }
}
