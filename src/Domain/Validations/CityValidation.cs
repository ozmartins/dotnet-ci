using FluentValidation;
using Domain.Interfaces.Filters;
using Domain.Interfaces.Validations;
using Domain.Interfaces;
using Domain.Models.Cities;

namespace Domain.Validations
{
    public class CityValidation : AbstractValidator<City>, ICityValidation
    {
        public CityValidation(IRepository<City> cityRepository, IFilterBuilder<City> filterBuilder)
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Nome da cidade não foi informado.");

            RuleFor(p => p.IbgeNumber).GreaterThan(1000000).WithMessage("O código IBGE precisa ter examente sete dígitos.");

            RuleFor(p => p.IbgeNumber).LessThan(9999999).WithMessage("O código IBGE precisa ter examente sete dígitos.");

            RuleFor(p => ibgeNumberAlreadyExists(cityRepository, filterBuilder, p)).Equal(false).WithMessage("Já existe uma cidade cadastrada com o código IBGE informado.");

            RuleFor(p => p.State).IsInEnum().WithMessage("O estado da cidade é inválido");

            RuleFor(p => (int)p.State).Equal(x => extractStateIdFromIbgeNumber(x.IbgeNumber)).WithMessage("O código IBGE informado não pertece ao estado informado.");
        }

        private static bool ibgeNumberAlreadyExists(IRepository<City> cityRepository, IFilterBuilder<City> filterBuilder, City city)
        {
            filterBuilder
                .Equal(x => x.IbgeNumber, city.IbgeNumber)
                .Unequal(x => x.Id, city.Id);

            return cityRepository.Recover(filterBuilder).Count > 0;
        }

        private static int extractStateIdFromIbgeNumber(int ibgeNumber)
        {
            var stateId = ibgeNumber / 100000; //IBGE number always has seven digits, so dividing for 100k, will return the two first digits.

            return stateId;
        }
    }
}