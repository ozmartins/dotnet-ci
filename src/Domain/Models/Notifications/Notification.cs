using Domain.Models.Notifications;

namespace Domain.Models.Notications
{
    public class Notification : Entity
    {
        public Notification() : base() { }
        public Notification(DateTime dateTime, PersonForNotification destination, string text)
        {
            DateTime = dateTime;
            Destination = destination;
            Text = text;
        }
        public DateTime DateTime { get; private set; }
        public PersonForNotification Destination { get; private set; } = new PersonForNotification();
        public string Text { get; private set; } = string.Empty;
    }
}
