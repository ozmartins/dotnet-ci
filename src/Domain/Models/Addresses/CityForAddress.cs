﻿using Domain.Models.Cities;

namespace Domain.Models.Addresses
{
    public class CityForAddress
    {
        public CityForAddress()
        {
        }

        public CityForAddress(Guid id, string name, Uf state)
        {
            Id = id;
            Name = name;
            State = state;
        }
        public Guid Id { get; private set; } = Guid.Empty;
        public string Name { get; private set; } = string.Empty;
        public Uf? State { get; private set; }
    }
}
