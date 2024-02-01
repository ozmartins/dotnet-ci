using AutoMapper;
using Api.Dtos.People;
using Api.Infra;
using Api.Interfaces.Mappers;
using Api.Views.People;
using Domain.Interfaces;
using Domain.Models.PaymentPlans;
using Domain.Models.People;

namespace Api.Mappers.People
{
    public class SupplierMapper : BaseMapper<Person>, ISupplierMapper
    {
        private readonly IMapper _autoMapper;

        private readonly IAddressMapper _addressMapper;

        private readonly IRepository<PaymentPlan> _paymentPlanRepository;

        public SupplierMapper(IMapper autoMapper, IAddressMapper addressMapper, IRepository<PaymentPlan> paymentPlanRepository)
        {
            _autoMapper = autoMapper;
            _addressMapper = addressMapper;
            _paymentPlanRepository = paymentPlanRepository;
        }

        public MapperResult<Person> Map(SupplierDto dto)
        {
            var person = new Person(
                dto.Name,
                dto.Document,
                SupplierOrCustomer.Supplier,
                new Customer(null),
                new Supplier(dto.BusinessDescription, new List<PaymentPlan>())
            );

            if (!SuccessResult())
            {
                return GetResult();
            }

            SetEntity(person);

            return GetResult();
        }

        public SupplierView? Map(Person? person)
        {
            return MapToView(person);
        }

        public List<SupplierView> Map(List<Person> people)
        {
            var suplliers = new List<SupplierView>();

            foreach (var person in people)
            {
                var mapped = MapToView(person);
                if (mapped != null)
                    suplliers.Add(mapped);
            }

            return suplliers;
        }

        private static SupplierView? MapToView(Person? person)
        {
            if (person == null) return null;

            var supplierView = new SupplierView()
            {
                Id = person.Id,
                Version = person.Version,
                Name = person.Name,
                Document = person.Document,
                BusinessDescription = person.SupplierInfo.BusinessDescription
            };

            return supplierView;
        }
    }
}
