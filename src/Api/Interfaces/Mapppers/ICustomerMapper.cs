using Api.Dtos.People;
using Api.Infra;
using Api.Views.People;
using Domain.Models.People;

namespace Api.Interfaces.Mappers
{
    public interface ICustomerMapper
    {
        MapperResult<Person> Map(CustomerDto dto);

        CustomerView? Map(Person? person);

        List<CustomerView> Map(List<Person> people);
    }
}
