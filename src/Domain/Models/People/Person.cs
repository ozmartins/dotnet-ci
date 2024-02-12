using Domain.Infra;
using Domain.Models.Addresses;

namespace Domain.Models.People
{
    public class Person : Entity
    {
        public Person() : base()
        {
        }

        public Person(string name, string document, SupplierOrCustomer supplierOrCustomer, Customer customerInfo, Supplier supplierInfo)
        {
            Name = name;
            Document = document;
            SupplierOrCustomer = supplierOrCustomer;
            CustomerInfo = customerInfo;
            SupplierInfo = supplierInfo;
        }
        public string Name { get; private set; } = string.Empty;
        public string Document { get; private set; } = string.Empty;
        public SupplierOrCustomer SupplierOrCustomer { get; private set; }
        public Customer CustomerInfo { get; private set; } = new Customer();
        public Supplier SupplierInfo { get; private set; } = new Supplier();
        public List<Address> Addresses { get; private set; } = new List<Address>();
        public List<Phone> Phones { get; private set; } = new List<Phone>();
        public void ReplacePhone(Guid phoneId, Phone newPhone)
        {
            var currentPhone = Phones.Find(x => x.Id == phoneId);

            if (currentPhone == null)
                throw new BusinessException("Não foi possível localizar o telefone informado");

            var index = Phones.IndexOf(currentPhone);

            Phones.Remove(currentPhone);

            Phones.Insert(index, newPhone);
        }
        public void RemovePhone(Guid phoneId)
        {
            var currentPhone = Phones.Find(x => x.Id == phoneId) ?? throw new BusinessException("Não foi possível localizar o telefone informado");
            Phones.Remove(currentPhone);
        }
        public void ReplaceAddress(Guid addressId, Address newAddress)
        {
            var currentAddress = Addresses.Find(x => x.Id == addressId);

            if (currentAddress == null)
                throw new BusinessException("Não foi possível localizar o endereço informado");

            var index = Addresses.IndexOf(currentAddress);

            Addresses.Remove(currentAddress);

            Addresses.Insert(index, newAddress);
        }
        public void RemoveAddress(Guid addressId)
        {
            var currentAddress = Addresses.Find(x => x.Id == addressId) ?? throw new BusinessException("Não foi possível localizar o endereço informado");
            Addresses.Remove(currentAddress);
        }
        public void RemovePaymentPlan(Guid paymentPlanId)
        {
            var currentPaymentPlan = SupplierInfo.PaymentPlans.Find(x => x.Id == paymentPlanId) ?? throw new BusinessException("Não foi possível localizar o plano de pagamento informado");
            SupplierInfo.PaymentPlans.Remove(currentPaymentPlan);
        }
    }
}
