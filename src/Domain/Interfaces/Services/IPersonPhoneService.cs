using Domain.Misc;
using Domain.Models.People;

namespace Domain.Interfaces.Services
{
    public interface IPersonPhoneService
    {
        public ServiceResult<Phone> AddPhone(Guid personId, Phone phone);

        public ServiceResult<Phone> ReplacePhone(Guid personId, Guid phoneId, Phone phone);

        public ServiceResult<Phone> RemovePhone(Guid personId, Guid phoneId);
    }
}
