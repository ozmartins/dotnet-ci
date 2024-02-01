namespace Api.Controllers.Constants
{
    public class ItemConstant
    {
        public const string Tag = "Item";

        public const string CreateSummary = "It creates an item in the supplier's portfolio.";
        public const string UpdateSummary = "It updates a supplier's item.";
        public const string DeleteSummary = "It removes an item from the supplier's portfolio.";
        public const string GetByIdSummary = "It gets a specific item from the supplier's portfolio.";
        public const string GetAllSummary = "It gets all items from the supplier's portfolio.";

        public const string CreateDescription = "Your app will have a page where the supplier will inform which items they sell or rent. This page can call this method to create the items. ";
        public const string DeleteDescription = "If the suppliers can add an item, they can remove it, and your app can use this method for it.";
        public const string UpdateDescription = "Suppliers have the right to change their items' information. Use this method to guarantee these rights.";
        public const string GetByIdDescription = "Your app will, probably, have a screen to see/edit the items' information and you will need this method to retrieve the item data.";
        public const string GetAllDescription = "There will be several screens on your app that will need to show an items list. This is the method that will help you on those screens.";
    }
}
