namespace Api.Views.Messages
{
    public class MessageView : View
    {
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public Guid OrderId { get; set; }
    }
}
