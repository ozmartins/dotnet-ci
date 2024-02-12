using Domain.Misc;
using Domain.Models.Addresses;
using Domain.Models.PaymentPlans;
using Domain.Models.People;

namespace Domain.Interfaces.Services
{
    public interface IPersonService : IService<Person>
    {
        public ServiceResult<Person> Create(Person person);

        public ServiceResult<Person> Update(Guid id, Person person);
    }
}
