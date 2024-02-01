namespace Api.Controllers.Constants
{
    public class ReservationConstant
    {
        public const string Tag = "Reservation";

        public const string CreateSummary = "Create a reservation entry to an item.";
        public const string UpdateSummary = "Update an item's reservation.";
        public const string DeleteSummary = "Remove an item's reservation.";
        public const string GetByIdSummary = "Get a specific item's reservation.";
        public const string GetAllSummary = "Get all the item's reservation.";

        public const string CreateDescription = "Honestly, this metohod shouldn't exist. Because the cities must be supplied by the internal team of the system.";
        public const string DeleteDescription = "Please, don't call this method. I'm begging you.";
        public const string UpdateDescription = "Please, don't call this method. I'm begging you.";
        public const string GetByIdDescription = "If you want to see details of the customer or supplier city, use this method.";
        public const string GetAllDescription = "If you want to create a search field that allows the user to find their own city, use this method.";
    }
}
