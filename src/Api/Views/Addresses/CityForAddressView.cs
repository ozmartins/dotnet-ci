using Domain.Models.Cities;

namespace Api.Views.Addresses
{
    public class CityForAddressView : View
    {
        public string Name { get; set; } = string.Empty;
        public Uf State { get; set; }
    }
}
