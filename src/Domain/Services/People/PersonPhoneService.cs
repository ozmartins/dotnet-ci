using Domain.Misc;
using Domain.Interfaces.Filters;
using Domain.Interfaces.Services;
using Domain.Interfaces.Validations;
using Domain.Models.People;
using Domain.Interfaces;

namespace Domain.Services.People
{
    public class PersonPhoneService : IPersonPhoneService
    {
        private readonly BasicService<Person> _basicService;

        private readonly IPhoneValidation _phoneValidation;

        private readonly IPersonPhoneValidation _personPhoneValidation;

        protected IFilterBuilder<Person> PersonFilterBuilder;

        protected IRepository<Person> Repository;

        public PersonPhoneService(IRepository<Person> repository,
                                  IFilterBuilder<Person> personFilterBuilder,
                                  IPersonValidation personValidation,
                                  IPhoneValidation phoneValidation,
                                  IPersonPhoneValidation personPhoneValidation)
        {
            _phoneValidation = phoneValidation;
            _personPhoneValidation = personPhoneValidation;
            _basicService = new BasicService<Person>(repository, personValidation);
            PersonFilterBuilder = personFilterBuilder;
            Repository = repository;
        }

        public ServiceResult<Phone> AddPhone(Guid personId, Phone phone)
        {
            var person = _basicService.Get(personId);

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
            var person = _basicService.Get(personId);

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
            var person = _basicService.Get(personId);

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
    }
}
