namespace Api.Views.Addresses
{
    public class AddressView : View
    {
        public string ZipCode { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public int Number { get; set; }
        public Guid CityId { get; set; }
    }
}
