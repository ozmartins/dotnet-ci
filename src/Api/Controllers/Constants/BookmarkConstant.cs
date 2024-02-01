namespace Api.Controllers.Constants
{
    public static class BookmarkConstant
    {
        public const string Tag = "Bookmark";

        public const string CreateSummary = "It creates a bookmark for a user.";
        public const string DeleteSummary = "It removes a bookmark.";
        public const string GetByIdSummary = "It gets a specific bookmark.";
        public const string GetAllSummary = "It gets all the bookmarks from the system.";

        public const string CreateDescription = "Typically you will have a star or heart icon in your app that will call this method to save a bookmark for the user.";
        public const string DeleteDescription = "That same star/heart icon will call this method to remove a user's bookmark.";
        public const string GetByIdDescription = "Honestly, I can't see a scenario where you will need to use this method.";
        public const string GetAllDescription = "Your app needs to retrieve the bookmark list to show it to the user. Use this method for it.";
    }
}
