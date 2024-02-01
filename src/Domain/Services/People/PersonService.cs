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
    public class PersonService : IPersonService
    {
        private BasicService<Person> _basicService;

        private IRepository<PaymentPlan> _paymentPlanRepository;

        private IPersonValidation _personValidation;

        private IPhoneValidation _phoneValidation;

        private IPersonPhoneValidation _personPhoneValidation;

        private IAddressValidation _addressValidation;

        private IPersonAddressValidation _personAddressValidation;

        protected IFilterBuilder<Person> PersonFilterBuilder;

        protected IRepository<Person> Repository;

        public PersonService(IRepository<Person> repository,
                             IFilterBuilder<Person> personFilterBuilder,
                             IPersonValidation personValidation,
                             IPhoneValidation phoneValidation,
                             IPersonPhoneValidation personPhoneValidation,
                             IAddressValidation addressValidation,
                             IPersonAddressValidation personAddressValidation,
                             IRepository<PaymentPlan> paymentPlanRepository)
        {
            _personValidation = personValidation;
            _phoneValidation = phoneValidation;
            _personPhoneValidation = personPhoneValidation;
            _addressValidation = addressValidation;
            _personAddressValidation = personAddressValidation;
            _paymentPlanRepository = paymentPlanRepository;
            _basicService = new BasicService<Person>(repository, personValidation);

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
            var person = Get(personId);

            if (person == null)
                return ServiceResult<Phone>.FailureResult("Não foi possível localizar a pessoa informada.");

            var result = _phoneValidation.Validate(phone);

            if (!result.IsValid)
                return ServiceResult<Phone>.FailureResult(result);

            person.Phones.Add(phone);

            result = _personPhoneValidation.Validate(person);

            if (!result.IsValid)
                return ServiceResult<Phone>.FailureResult(result);

            Repository.Update(personId, person);

            return ServiceResult<Phone>.SuccessResult(phone);
        }

        public ServiceResult<Phone> ReplacePhone(Guid personId, Guid phoneId, Phone phone)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<Phone>.FailureResult("Não foi possível localizar a pessoa informada.");

            var result = _phoneValidation.Validate(phone);

            if (!result.IsValid)
                return ServiceResult<Phone>.FailureResult(result);

            person.ReplacePhone(phoneId, phone);

            result = _personPhoneValidation.Validate(person);

            if (!result.IsValid)
                return ServiceResult<Phone>.FailureResult(result);

            Repository.Update(personId, person);

            return ServiceResult<Phone>.SuccessResult(phone);
        }

        public ServiceResult<Phone> RemovePhone(Guid personId, Guid phoneId)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<Phone>.FailureResult("Não foi possível localizar a pessoa informada.");

            var phone = person.Phones.Find(x => x.Id.Equals(phoneId)) ?? new Phone();

            person.RemovePhone(phoneId);

            var result = _personPhoneValidation.Validate(person);

            if (!result.IsValid)
                return ServiceResult<Phone>.FailureResult(result);

            Repository.Update(personId, person);

            return ServiceResult<Phone>.SuccessResult(phone);
        }

        public ServiceResult<Address> AddAddress(Guid personId, Address address)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<Address>.FailureResult("Não foi possível localizar a pessoa informada.");

            var result = _addressValidation.Validate(address);

            if (!result.IsValid)
                return ServiceResult<Address>.FailureResult(result);

            person.Addresses.Add(address);

            result = _personAddressValidation.Validate(person);

            if (!result.IsValid)
                return ServiceResult<Address>.FailureResult(result);

            Repository.Update(personId, person);

            return ServiceResult<Address>.SuccessResult(address);
        }

        public ServiceResult<Address> ReplaceAddress(Guid personId, Guid addressId, Address address)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<Address>.FailureResult("Não foi possível localizar a pessoa informada.");

            var result = _addressValidation.Validate(address);

            if (!result.IsValid)
                return ServiceResult<Address>.FailureResult(result);

            result = _personAddressValidation.Validate(person);

            if (!result.IsValid)
                return ServiceResult<Address>.FailureResult(result);

            person.ReplaceAddress(addressId, address);

            Repository.Update(personId, person);

            return ServiceResult<Address>.SuccessResult(address);
        }

        public ServiceResult<Address> RemoveAddress(Guid personId, Guid addressId)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<Address>.FailureResult("Não foi possível localizar a pessoa informada.");

            var address = person.Addresses.Find(x => x.Id.Equals(addressId)) ?? new Address();

            person.RemoveAddress(addressId);

            Repository.Update(personId, person);

            return ServiceResult<Address>.SuccessResult(address);
        }

        public ServiceResult<PaymentPlan> AddPaymentPlan(Guid personId, Guid paymentPlanId)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<PaymentPlan>.FailureResult("Não foi possível localizar a pessoa informada.");

            var paymentPlan = _paymentPlanRepository.RecoverById(paymentPlanId);

            if (paymentPlan == null)
                return ServiceResult<PaymentPlan>.FailureResult("Não foi possível localizar o plano de pagamento informado.");

            if (person.SupplierInfo.PaymentPlans.Exists(x => x.Id == paymentPlan.Id))
                return ServiceResult<PaymentPlan>.FailureResult("O plano de pagamento informado já está vinculado ao fornecedor.");

            person.SupplierInfo.PaymentPlans.Add(paymentPlan);

            Repository.Update(personId, person);

            return ServiceResult<PaymentPlan>.SuccessResult(paymentPlan);
        }

        public ServiceResult<PaymentPlan> RemovePaymentPlan(Guid personId, Guid paymentPlanId)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<PaymentPlan>.FailureResult("Não foi possível localizar a pessoa informada.");

            var paymentPlan = person.SupplierInfo.PaymentPlans.Find(x => x.Id.Equals(paymentPlanId)) ?? new PaymentPlan();

            person.RemovePaymentPlan(paymentPlanId);

            Repository.Update(personId, person);

            return ServiceResult<PaymentPlan>.SuccessResult(paymentPlan);
        }
    }
}
