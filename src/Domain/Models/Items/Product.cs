namespace Domain.Models.Items
{
    public class Product
    {
        public Product() { }
        public Product(decimal availableQuantity, RentOrSale forRentOrSale)
        {
            AvailableQuantity = availableQuantity;
            ForRentOrSale = forRentOrSale;
        }
        public decimal AvailableQuantity { get; private set; }
        public RentOrSale ForRentOrSale { get; private set; }
        public void IncreaseQauntity(decimal quantity)
        {
            AvailableQuantity += quantity;
        }
        public void DecreaseQauntity(decimal quantity)
        {
            AvailableQuantity -= quantity;
        }
    }
}
