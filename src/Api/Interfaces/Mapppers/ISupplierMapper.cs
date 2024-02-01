using Api.Dtos.People;
using Api.Infra;
using Api.Views.People;
using Domain.Models.People;

namespace Api.Interfaces.Mappers
{
    public interface ISupplierMapper
    {
        MapperResult<Person> Map(SupplierDto dto);

        SupplierView? Map(Person? person);

        List<SupplierView> Map(List<Person> people);
    }
}
