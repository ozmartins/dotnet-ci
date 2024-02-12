using AutoMapper;
using Api.Dtos.People;
using Api.Infra;
using Api.Interfaces.Mappers;
using Api.Views.People;
using Domain.Models.PaymentPlans;
using Domain.Models.People;

namespace Api.Mappers.People
{
    public class CustomerMapper : BaseMapper<Person>, ICustomerMapper
    {
        private readonly IMapper _autoMapper;

        private readonly IAddressMapper _addressMapper;

        public CustomerMapper(IMapper autoMapper, IAddressMapper addressMapper)
        {
            _autoMapper = autoMapper;
            _addressMapper = addressMapper;
        }

        public MapperResult<Person> Map(CustomerDto dto)
        {
            var person = new Person
            (
                dto.Name,
                dto.Document,
                SupplierOrCustomer.Customer,
                new Customer(dto.BirthDate == DateTime.MinValue ? null : dto.BirthDate),
                new Supplier(string.Empty, new List<PaymentPlan>())
            );

            if (!SuccessResult())
            {
                return GetResult();
            }

            SetEntity(person);

            return GetResult();
        }

        public CustomerView? Map(Person? person)
        {
            return MapToView(person);
        }

        public List<CustomerView> Map(List<Person> people)
        {
            var customers = new List<CustomerView>();

            foreach (var person in people)
            {
                var mapped = MapToView(person);
                if (mapped != null)
                    customers.Add(mapped);
            }

            return customers;
        }

        private static CustomerView? MapToView(Person? person)
        {
            if (person == null) return null;

            var customerView = new CustomerView()
            {
                Id = person.Id,
                Version = person.Version,
                Name = person.Name,
                Document = person.Document,
                BirthDate = person.CustomerInfo.BirthDate
            };

            return customerView;
        }
    }
}
