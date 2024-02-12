using Domain.Misc;
using Domain.Models.Addresses;

namespace Domain.Interfaces.Services
{
    public interface IPersonAddressService
    {
        public ServiceResult<Address> AddAddress(Guid personId, Address address);

        public ServiceResult<Address> ReplaceAddress(Guid personId, Guid addressId, Address address);

        public ServiceResult<Address> RemoveAddress(Guid personId, Guid addressId);
    }
}
