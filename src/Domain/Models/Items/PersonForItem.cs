using Domain.Models.People;

namespace Domain.Models.Items
{
    public class PersonForItem
    {
        public PersonForItem()
        {
        }
        public PersonForItem(Guid id, string name, SupplierOrCustomer supplierOrCustomer)
        {
            Id = id;
            Name = name;
            SupplierOrCustomer = supplierOrCustomer;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public SupplierOrCustomer SupplierOrCustomer { get; private set; }
    }
}