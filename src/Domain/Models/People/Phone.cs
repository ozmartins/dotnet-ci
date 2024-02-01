namespace Domain.Models.People
{
    public class Phone : Entity
    {
        public Phone() : base()
        {
        }

        public Phone(Guid id, string prefix, string number) : base(id, Guid.NewGuid(), false)
        {
            Prefix = prefix;
            Number = number;
        }

        public string Prefix { get; private set; } = string.Empty;
        public string Number { get; private set; } = string.Empty;

    }
}
