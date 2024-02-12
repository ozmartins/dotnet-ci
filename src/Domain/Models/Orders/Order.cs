using Domain.Infra;
using Domain.Models.Addresses;

namespace Domain.Models.Orders
{
    public class Order : Entity
    {
        public Order() : base() { }
        public Order(PersonForOrder supplier, PersonForOrder customer, decimal freight, string notes, DateTime partyDate, List<OrderItem> items)
        {
            Supplier = supplier;
            Customer = customer;
            Freight = freight;
            Notes = notes;
            PartyDate = partyDate;
            Items = items;
        }
        public DateTime DateTime { get; private set; }
        public PersonForOrder Supplier { get; private set; } = new PersonForOrder();
        public PersonForOrder Customer { get; private set; } = new PersonForOrder();
        public Address? ShippingAddress { get; private set; } = new Address();
        public decimal Freight { get; private set; }
        public decimal PaymentPlanFee { get; private set; }
        public decimal ItemsTotal { get; private set; }
        public decimal OrderTotal { get; private set; }
        public PaymentPlanForOrder? PaymentPlan { get; private set; } = new PaymentPlanForOrder();
        public string Notes { get; private set; } = string.Empty;
        public OrderStatus Status { get; private set; }
        public DateTime PartyDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public List<OrderItem> Items { get; private set; } = new List<OrderItem>();
        public static DateTime CalculateExpirationDate(DateTime baseDateTime)
        {
            return baseDateTime.AddDays(7);
        }
        public void DefineShippingAddress(Address shippingAddress)
        {
            ShippingAddress = shippingAddress;
        }
        public void DefinePaymentPlan(PaymentPlanForOrder paymentPlan)
        {
            PaymentPlan = paymentPlan;
        }
        public void SetDefaultValuesForNewOrder()
        {
            DateTime = DateTime.Now;

            ExpirationDate = CalculateExpirationDate(DateTime);

            Status = OrderStatus.Draft;
        }
        public void CopyHeaderData(Order source)
        {
            Supplier = source.Supplier;
            Customer = source.Customer;
            ShippingAddress = source.ShippingAddress;
            Freight = source.Freight;
            PaymentPlan = source.PaymentPlan;
            Notes = source.Notes;
            PartyDate = source.PartyDate;
        }
        public void CopyItemsData(Order newOrder)
        {
            foreach (var item in Items)
            {
                item.CopyData(newOrder.Items.Find(x => x.Item.Id == item.Item.Id)!);
            }
        }
        public decimal CalculateItemsTotal()
        {
            return Items.Sum(x => x.Total);
        }
        public decimal CalculatePaymentPlanFee()
        {
            return Math.Round((ItemsTotal + Freight) * (PaymentPlan?.Fee ?? 0) / 100, 2);
        }
        public decimal CalculateOrderTotal()
        {
            return CalculateItemsTotal() + Freight + PaymentPlanFee;
        }
        public void TotalizeOrder()
        {
            foreach (var item in Items)
            {
                item.TotalizeOrderItem();
            }

            ItemsTotal = CalculateItemsTotal();

            PaymentPlanFee = CalculatePaymentPlanFee();

            OrderTotal = CalculateOrderTotal();
        }
        public void ReplaceItem(Guid currentOrderItemId, OrderItem newOrderItem)
        {
            var currentOrderItem = Items.Find(x => x.Id == currentOrderItemId);

            if (currentOrderItem == null)
                throw new BusinessException("Não foi possível localizar o item de pedido informado.");

            var index = Items.IndexOf(currentOrderItem);

            Items.Remove(currentOrderItem);

            newOrderItem.DefineIdAndVersion(currentOrderItemId, Guid.NewGuid());

            Items.Insert(index, newOrderItem);
        }
        internal void RemoveItem(Guid orderItemId)
        {
            var currentOrderItem = Items.Find(x => x.Id == orderItemId) ?? throw new BusinessException("Não foi possível localizar o item de pedido informado");
            Items.Remove(currentOrderItem);
        }
    }
}
