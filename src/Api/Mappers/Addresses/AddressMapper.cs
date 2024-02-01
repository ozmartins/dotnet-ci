using Api.Dtos.Addresses;
using Api.Infra;
using Api.Interfaces.Mappers;
using Domain.Infra.Extensions;
using Domain.Models.Addresses;
using Domain.Interfaces;
using Domain.Models.Cities;
using Api.Views.Addresses;

namespace Api.Mappers.Addresses
{
    public class AddressMapper : BaseMapper<Address>, IAddressMapper
    {
        private IRepository<City> _cityRepository;

        public AddressMapper(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public MapperResult<Address> Map(AddressDto dto)
        {
            var city = _cityRepository.RecoverById(dto.CityId).IfNull(() => AddError("A cidade do endereço não foi encontrada"));

            if (!SuccessResult()) return GetResult();

            SetEntity(new Address(dto.ZipCode, dto.Street, dto.Number, dto.District, new CityForAddress(city!.Id, city!.Name, city.State)));

            return GetResult();
        }

        public List<MapperResult<Address>> Map(List<AddressDto> dtos)
        {
            var result = new List<MapperResult<Address>>();

            foreach (var dto in dtos)
            {
                //this.ClearResult(); TODO: Rever
                result.Add(Map(dto));
            }

            return result;
        }

        public AddressView? Map(Address? adddress)
        {
            if (adddress == null) return null;

            var view = new AddressView
            {
                Id = adddress.Id,
                Version = adddress.Version,
                ZipCode = adddress.ZipCode,
                Street = adddress.Street,
                District = adddress.District,
                Number = adddress.Number,
                CityId = adddress.City.Id
            };

            return view;
        }

        public List<AddressView> Map(List<Address> addresses)
        {
            var result = new List<AddressView>();

            foreach (var address in addresses)
            {
                var addressView = new AddressView
                {
                    Id = address.Id,
                    Version = address.Version,
                    ZipCode = address.ZipCode,
                    CityId = address.City.Id,
                    District = address.District,
                    Street = address.Street,
                    Number = address.Number,
                };

                result.Add(addressView);
            }

            return result;
        }
    }
}
