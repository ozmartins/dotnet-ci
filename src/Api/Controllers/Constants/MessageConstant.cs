namespace Api.Controllers.Constants
{
    public class MessageConstant
    {
        public const string Tag = "Message";

        public const string CreateSummary = "It creates a message from a customer to a supplier or vice versa.";
        public const string UpdateSummary = "It updates a message.";
        public const string DeleteSummary = "It removes a message.";
        public const string GetByIdSummary = "It gets a specific message.";
        public const string GetAllSummary = "It gets all the messages.";

        public const string CreateDescription = "Eventually, a customer will send a question to a supplier (or vice versa). Your app should use this method to send this message. ";
        public const string DeleteDescription = "Once it was is sent, a message shouldn't be removed. So, this method doesn't make sense.";
        public const string UpdateDescription = "Once it was is sent, a message shouldn't be updated. So, this method doesn't make sense.";
        public const string GetByIdDescription = "Your app will probably have a screen to show to message to the user and this method will help you with it.";
        public const string GetAllDescription = "Your app will probably have a screen to list all the user's messages and this method will help you with it.";
    }
}
