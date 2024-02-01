using Domain.Interfaces;
using Domain.Interfaces.Filters;
using Domain.Models.Addresses;
using Domain.Validations;
using Infra.Repositories;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace iParty.Test.Business.Addresses
{
    public class CityTest
    {
        [Fact]
        public void SendingIbgeNumberInvalidReturnErrorMessageIbge()
        {
            //var cityRepository = new Mock<IRepository<City>>();
            //cityRepository.Setup(x => x.Recover(It.IsAny<IFilterBuilder<City>>())).Returns(new List<City>());
            //var validation = new CityValidation(cityRepository.Object, new FilterBuilder<City>());

            //var result = validation.Validate(new City
            //{
            //    IbgeNumber = 0
            //});

            //Assert.Contains(result.Errors, p => p.ErrorMessage == "O código IBGE precisa ter examente sete dígitos.");
        }

        [Fact]
        public void SendingIbgeNumberValidShouldNotReturnErrorMessageIbge()
        {
            //var cityRepository = new Mock<IRepository<City>>();
            //cityRepository.Setup(x => x.Recover(It.IsAny<IFilterBuilder<City>>())).Returns(new List<City>());
            //var validation = new CityValidation(cityRepository.Object, new FilterBuilder<City>());

            //var result = validation.Validate(new City
            //{
            //    IbgeNumber = 1111111
            //});

            //Assert.DoesNotContain(result.Errors, p => p.ErrorMessage == "O código IBGE precisa ter examente sete dígitos.");
        }
    }
}
