using Domain.Models.Cities;

namespace Api.Views.Cities
{
    public class CityView : View
    {
        public int IbgeNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public UfEnum State { get; set; }
    }
}
