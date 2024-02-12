using Domain.Misc;
using Domain.Interfaces.Filters;
using Domain.Interfaces.Services;
using Domain.Interfaces.Validations;
using Domain.Models.Addresses;
using Domain.Models.People;
using Domain.Interfaces;

namespace Domain.Services.People
{
    public class PersonAddressService : IPersonAddressService
    {
        private readonly BasicService<Person> _basicService;

        private readonly IAddressValidation _addressValidation;

        private readonly IPersonAddressValidation _personAddressValidation;

        protected IFilterBuilder<Person> PersonFilterBuilder;

        protected IRepository<Person> Repository;

        public PersonAddressService(IRepository<Person> repository,
                                    IFilterBuilder<Person> personFilterBuilder,
                                    IPersonValidation personValidation,
                                    IAddressValidation addressValidation,
                                    IPersonAddressValidation personAddressValidation)
        {
            _addressValidation = addressValidation;
            _personAddressValidation = personAddressValidation;
            _basicService = new BasicService<Person>(repository, personValidation);
            PersonFilterBuilder = personFilterBuilder;
            Repository = repository;
        }

        public ServiceResult<Address> AddAddress(Guid personId, Address address)
        {
            var person = _basicService.Get(personId);

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
            var person = _basicService.Get(personId);

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
            var person = _basicService.Get(personId);

            if (person == null)
                return ServiceResult<Address>.FailureResult("Não foi possível localizar a pessoa informada.");

            var address = person.Addresses.Find(x => x.Id.Equals(addressId)) ?? new Address();

            person.RemoveAddress(addressId);

            Repository.Update(personId, person);

            return ServiceResult<Address>.SuccessResult(address);
        }
    }
}
