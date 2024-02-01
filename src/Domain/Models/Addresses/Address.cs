namespace Domain.Models.Addresses
{
    public class Address : Entity
    {
        public Address() : base() { }

        public Address(string zipCode, string street, int number, string district, CityForAddress city)
        {
            ZipCode = zipCode;
            Street = street;
            Number = number;
            District = district;
            City = city;
        }

        public string ZipCode { get; private set; } = string.Empty;
        public string Street { get; private set; } = string.Empty;
        public int Number { get; private set; } = 0;
        public string District { get; private set; } = string.Empty;
        public CityForAddress City { get; private set; } = new CityForAddress();
    }
}
