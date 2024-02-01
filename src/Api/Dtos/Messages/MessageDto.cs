namespace Api.Dtos.Messages
{
    public class MessageDto
    {
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public string Text { get; set; } = string.Empty;
        public Guid? OrderId { get; set; }
    }
}
