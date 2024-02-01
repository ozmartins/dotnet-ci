using Api.Dtos.Addresses;
using Api.Infra;
using Api.Views.Addresses;
using Domain.Models.Addresses;

namespace Api.Interfaces.Mappers
{
    public interface IAddressMapper
    {
        public MapperResult<Address> Map(AddressDto dto);

        public List<MapperResult<Address>> Map(List<AddressDto> dtos);

        public AddressView? Map(Address? domain);

        public List<AddressView> Map(List<Address> domains);
    }
}
