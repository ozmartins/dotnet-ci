namespace Domain.Models.Messages
{
    public class PersonForMessage
    {
        public PersonForMessage() { }
        public PersonForMessage(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
    }
}
