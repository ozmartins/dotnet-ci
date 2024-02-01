namespace Domain.Models.Notifications
{
    public class PersonForNotification
    {
        public PersonForNotification() { }
        public PersonForNotification(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
    }
}
