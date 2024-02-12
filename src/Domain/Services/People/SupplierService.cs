using Domain.Interfaces.Filters;
using Domain.Interfaces.Services;
using Domain.Interfaces.Validations;
using Domain.Models.PaymentPlans;
using Domain.Models.People;
using Domain.Interfaces;

namespace Domain.Services.People
{
    public class SupplierService : PersonService, ISupplierService
    {
        public SupplierService(IRepository<Person> repository,
                               IFilterBuilder<Person> personFilterBuilder,
                               IPersonValidation personValidation,
                               IPersonPhoneService personPhoneService,
                               IPersonAddressService personAddressService,
                               IPersonPaymentPlanService personPaymentPlanService) :
            base(
                repository,
                personFilterBuilder,
                personValidation,
                personPhoneService,
                personAddressService,
                personPaymentPlanService)
        {
        }

        public override Person Get(Guid id)
        {
            PersonFilterBuilder.Clear();

            PersonFilterBuilder
                .Equal(x => x.Id, id)
                .Equal(x => x.SupplierOrCustomer, SupplierOrCustomer.Supplier);

            return Repository.Recover(PersonFilterBuilder).FirstOrDefault() ?? new Person();
        }

        public override List<Person> Get()
        {
            PersonFilterBuilder.Clear();

            PersonFilterBuilder.Equal(x => x.SupplierOrCustomer, SupplierOrCustomer.Supplier);

            return Repository.Recover(PersonFilterBuilder);
        }
    }
}
