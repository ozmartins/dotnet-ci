namespace Api.Dtos.Reviews
{
    public class ReviewDto
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Stars { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
    }
}
