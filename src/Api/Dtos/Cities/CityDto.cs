using Domain.Models.Cities;

namespace Api.Dtos.Cities
{
    public class CityDto
    {
        public int IbgeNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public UfEnum State { get; set; }
    }
}
