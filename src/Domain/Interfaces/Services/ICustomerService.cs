﻿namespace Domain.Interfaces.Services
{
    public interface ICustomerService : IPersonService, IPersonPhoneService, IPersonAddressService, IPersonPaymentPlanService
    {
    }
}
