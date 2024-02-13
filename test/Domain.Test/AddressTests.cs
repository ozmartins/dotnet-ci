using Xunit;
using System;
using FluentAssertions;
using Domain.Models.Cities;
using Domain.Models.Addresses;

namespace Domain.Test;

public class AddressTests
{
    [Fact]
    public void Test1()
    {
        // Given
        var zipCode = "123456789";
        var street = "Rua";
        var number = 1;
        var district = "Bairro";

        var cityId = Guid.NewGuid();
        var cityName = "Cidade";
        var state = Uf.AC;
        var city = new CityForAddress(cityId, cityName, state);

        // When
        var address = new Address(zipCode, street, number, district, city);

        // Then
        address.ZipCode.Should().Be(zipCode);
        address.Street.Should().Be(street);
        address.Number.Should().Be(number);
        address.District.Should().Be(district);
        address.City.Id.Should().Be(city.Id);
        address.City.Name.Should().Be(city.Name);
        address.City.State.Should().Be(city.State);
    }
}