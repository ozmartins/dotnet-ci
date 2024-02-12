using Domain.Misc;
using Domain.Interfaces.Filters;
using Domain.Interfaces.Services;
using Domain.Interfaces.Validations;
using Domain.Models.PaymentPlans;
using Domain.Models.People;
using Domain.Interfaces;

namespace Domain.Services.People
{
    public class PersonPaymentPlanService : IPersonPaymentPlanService
    {
        private readonly BasicService<Person> _basicService;

        private readonly IRepository<PaymentPlan> _paymentPlanRepository;

        protected IFilterBuilder<Person> PersonFilterBuilder;

        protected IRepository<Person> Repository;

        public PersonPaymentPlanService(IRepository<Person> repository,
                                        IFilterBuilder<Person> personFilterBuilder,
                                        IPersonValidation personValidation,
                                        IRepository<PaymentPlan> paymentPlanRepository)
        {
            _paymentPlanRepository = paymentPlanRepository;
            _basicService = new BasicService<Person>(repository, personValidation);
            PersonFilterBuilder = personFilterBuilder;
            Repository = repository;
        }

        public ServiceResult<PaymentPlan> AddPaymentPlan(Guid personId, Guid paymentPlanId)
        {
            var person = _basicService.Get(personId);

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
            var person = _basicService.Get(personId);

            if (person == null)
                return ServiceResult<PaymentPlan>.FailureResult("Não foi possível localizar a pessoa informada.");

            var paymentPlan = person.SupplierInfo.PaymentPlans.Find(x => x.Id.Equals(paymentPlanId)) ?? new PaymentPlan();

            person.RemovePaymentPlan(paymentPlanId);

            Repository.Update(personId, person);

            return ServiceResult<PaymentPlan>.SuccessResult(paymentPlan);
        }
    }
}
