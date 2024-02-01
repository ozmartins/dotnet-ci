namespace Api.Dtos.Notifications
{
    public class NotificationDto
    {
        public Guid DestinationId { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
