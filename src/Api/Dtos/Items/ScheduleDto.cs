namespace Api.Dtos.Items
{
    public class ScheduleDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public List<ScheduleItemDto> Items { get; set; } = new List<ScheduleItemDto>();
    }
}
