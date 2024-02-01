namespace Domain.Models.Review
{
    public class ItemForOrderItemForReview
    {
        public ItemForOrderItemForReview() { }
        public ItemForOrderItemForReview(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;
    }
}
