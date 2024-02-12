namespace Domain.Models.Cities
{
    public class City : Entity
    {
        public City() : base() { }
        public City(int ibgeNumber, string name, Uf state)
        {
            IbgeNumber = ibgeNumber;
            Name = name;
            State = state;
        }
        public int IbgeNumber { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Uf State { get; private set; }
    }
}
