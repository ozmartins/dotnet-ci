namespace Api.Dtos.Addresses
{
    public class AddressDto
    {
        public string ZipCode { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;

        public int Number { get; set; }
        public string District { get; set; } = string.Empty;
        public Guid CityId { get; set; }
    }
}
