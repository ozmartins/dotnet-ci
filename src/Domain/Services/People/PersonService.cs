using Domain.Misc;
using Domain.Interfaces.Filters;
using Domain.Interfaces.Services;
using Domain.Interfaces.Validations;
using Domain.Models.Addresses;
using Domain.Models.PaymentPlans;
using Domain.Models.People;
using Domain.Interfaces;

namespace Domain.Services.People
{
    public class PersonService : IPersonService, IPersonPhoneService, IPersonAddressService, IPersonPaymentPlanService
    {
        private readonly BasicService<Person> _basicService;

        private readonly IPersonPhoneService _personPhoneService;

        private readonly IPersonAddressService _personAddressService;

        private readonly IPersonPaymentPlanService _personPaymentPlanService;

        protected IFilterBuilder<Person> PersonFilterBuilder;

        protected IRepository<Person> Repository;

        public PersonService(IRepository<Person> repository,
                             IFilterBuilder<Person> personFilterBuilder,
                             IPersonValidation personValidation,
                             IPersonPhoneService personPhoneService,
                             IPersonAddressService personAddressService,
                             IPersonPaymentPlanService personPaymentPlanService)
        {
            _basicService = new BasicService<Person>(repository, personValidation);
            _personPhoneService = personPhoneService;
            _personAddressService = personAddressService;
            _personPaymentPlanService = personPaymentPlanService;

            PersonFilterBuilder = personFilterBuilder;
            Repository = repository;
        }

        public ServiceResult<Person> Create(Person person)
        {
            return _basicService.Create(person);
        }

        public ServiceResult<Person> Update(Guid id, Person person)
        {
            return _basicService.Update(id, person);
        }

        public ServiceResult<Person> Delete(Guid id)
        {
            return _basicService.Delete(id);
        }

        public virtual Person Get(Guid id)
        {
            return _basicService.Get(id);
        }

        public virtual List<Person> Get()
        {
            return _basicService.Get();
        }

        public ServiceResult<Phone> AddPhone(Guid personId, Phone phone)
        {
            return _personPhoneService.AddPhone(personId, phone);
        }

        public ServiceResult<Phone> ReplacePhone(Guid personId, Guid phoneId, Phone phone)
        {
            return _personPhoneService.ReplacePhone(personId, phoneId, phone);
        }

        public ServiceResult<Phone> RemovePhone(Guid personId, Guid phoneId)
        {
            return _personPhoneService.RemovePhone(personId, phoneId);
        }

        public ServiceResult<Address> AddAddress(Guid personId, Address address)
        {
            return _personAddressService.AddAddress(personId, address);
        }

        public ServiceResult<Address> ReplaceAddress(Guid personId, Guid addressId, Address address)
        {
            return _personAddressService.ReplaceAddress(personId, addressId, address);
        }

        public ServiceResult<Address> RemoveAddress(Guid personId, Guid addressId)
        {
            return _personAddressService.RemoveAddress(personId, addressId);
        }

        public ServiceResult<PaymentPlan> AddPaymentPlan(Guid personId, Guid paymentPlanId)
        {
            return _personPaymentPlanService.AddPaymentPlan(personId, paymentPlanId);
        }

        public ServiceResult<PaymentPlan> RemovePaymentPlan(Guid personId, Guid paymentPlanId)
        {
            return _personPaymentPlanService.RemovePaymentPlan(personId, paymentPlanId);
        }
    }
}
