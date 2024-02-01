namespace Api.Controllers.Constants
{
    public class ScheduleConstant
    {
        public const string Tag = "Item";

        public const string CreateSummary = "It adds a schedule to an item.";
        public const string UpdateSummary = "It updates an item's schedule.";
        public const string DeleteSummary = "It removes a schedule from an item.";
        public const string GetByIdSummary = "It gets a specific schedule from an item.";
        public const string GetAllSummary = "It gets all schedules from an item.";

        public const string CreateDescription = "If you decide to allow rentable items, you will need to create a calendar for suppliers to say the date and time the item is available for rent. Use this method to persist these dates and times.";
        public const string UpdateDescription = "This method shouldn't exist, because you have to create and remove a schedule, not change it.";
        public const string DeleteDescription = "Suppliers have the right to remove a schedule and you will use this method for it.";
        public const string GetByIdDescription = "Your app will, probably, have a screen to see/edit the items' schedules and you will need this method to retrieve this kind of data.";
        public const string GetAllDescription = "There will be some screens on your app that will need to show an schedules list. This is the method that will help you on those screens.";

    }
}
