namespace Domain.Models.People
{
    public class Customer
    {
        public Customer() { }
        public Customer(DateTime? birthDate)
        {
            BirthDate = birthDate;
        }
        public DateTime? BirthDate { get; private set; }
    }
}
