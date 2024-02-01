using Domain.Interfaces.Filters;
using Domain.Interfaces.Services;
using Domain.Interfaces.Validations;
using Domain.Models.PaymentPlans;
using Domain.Models.People;
using Domain.Interfaces;

namespace Domain.Services.People
{
    public class CustomerService : PersonService, ICustomerService
    {
        public CustomerService(IRepository<Person> rep, IFilterBuilder<Person> personFilterBuilder, IPersonValidation personValidation, IPhoneValidation phoneValidation, IPersonPhoneValidation personPhoneValidation, IAddressValidation addressValidation, IPersonAddressValidation personAddressValidation, IRepository<PaymentPlan> paymentPlanRepository) : base(rep, personFilterBuilder, personValidation, phoneValidation, personPhoneValidation, addressValidation, personAddressValidation, paymentPlanRepository)
        {
        }

        public override Person Get(Guid id)
        {
            PersonFilterBuilder.Clear();

            PersonFilterBuilder
                .Equal(x => x.Id, id)
                .Equal(x => x.SupplierOrCustomer, SupplierOrCustomer.Customer);

            return Repository.Recover(PersonFilterBuilder).FirstOrDefault() ?? new Person();
        }

        public override List<Person> Get()
        {
            PersonFilterBuilder.Clear();

            PersonFilterBuilder.Equal(x => x.SupplierOrCustomer, SupplierOrCustomer.Customer);

            return Repository.Recover(PersonFilterBuilder);
        }
    }
}
