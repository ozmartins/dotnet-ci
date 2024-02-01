namespace Domain.Models.Orders
{
    public class PersonForOrder : Entity
    {
        public PersonForOrder() { }
        public PersonForOrder(string name, string document)
        {
            Name = name;
            Document = document;
        }
        public string Name { get; private set; } = string.Empty;
        public string Document { get; private set; } = string.Empty;
    }
}
