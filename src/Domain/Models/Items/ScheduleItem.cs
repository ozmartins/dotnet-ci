namespace Domain.Models.Items
{
    public class ScheduleItem : Entity
    {
        public ScheduleItem() : base() { }
        public ScheduleItem(int initialHour, int finalHour)
        {
            InitialHour = initialHour;
            FinalHour = finalHour;
        }
        public int InitialHour { get; private set; }
        public int FinalHour { get; private set; }
    }
}
