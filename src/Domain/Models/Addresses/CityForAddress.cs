using Domain.Models.Cities;

namespace Domain.Models.Addresses
{
    public class CityForAddress
    {
        public CityForAddress()
        {
        }

        public CityForAddress(Guid id, string name, UfEnum state)
        {
            Id = id;
            Name = name;
            State = state;
        }
        public Guid Id { get; private set; } = Guid.Empty;
        public string Name { get; private set; } = string.Empty;
        public UfEnum? State { get; private set; }
    }
}
