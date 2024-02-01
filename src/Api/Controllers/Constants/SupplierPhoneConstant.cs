namespace Api.Controllers.Constants
{
    public class SupplierPhoneConstant
    {
        public const string Tag = "Supplier";

        public const string CreateSummary = "Add a phone to a supplier.";
        public const string UpdateSummary = "Update a supplier's phone.";
        public const string DeleteSummary = "Remove a phone from a supplier.";
        public const string GetByIdSummary = "It gets a specific phone from a specific supplier.";
        public const string GetAllSummary = "It gets all phones from a specific supplier.";

        public const string CreateDescription = "Honestly, this metohod shouldn't exist. Because the cities must be supplied by the internal team of the system.";
        public const string DeleteDescription = "Please, don't call this method. I'm begging you.";
        public const string UpdateDescription = "Please, don't call this method. I'm begging you.";
        public const string GetByIdDescription = "This is the method your app will use to show one specific supplier phone. I can't see a scenario where this can be useful.";
        public const string GetAllDescription = "This is the method your app will use to show all supplier phones. It can be used by the user themself or by customers.";
    }
}
