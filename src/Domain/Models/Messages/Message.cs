namespace Domain.Models.Messages
{
    public class Message : Entity
    {
        public Message() : base() { }
        public Message(PersonForMessage from, PersonForMessage to, string text, DateTime dateTime, Guid? orderId)
        {
            From = from;
            To = to;
            Text = text;
            DateTime = dateTime;
            OrderId = orderId;
        }
        public PersonForMessage From { get; private set; } = new PersonForMessage();
        public PersonForMessage To { get; private set; } = new PersonForMessage();
        public string Text { get; private set; } = string.Empty;
        public DateTime DateTime { get; private set; }
        public Guid? OrderId { get; private set; }

    }
}
