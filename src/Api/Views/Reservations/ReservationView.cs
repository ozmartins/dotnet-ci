using Domain.Models.Reservation;

namespace Api.Views.Reservations
{
    public class ReservationView : View
    {
        public DateTime Date { get; set; }
        public int InitialHour { get; set; }
        public int FinalHour { get; set; }
        public Guid ItemId { get; set; }
        public Guid OrderItemId { get; set; }
        public ReservationReason ReservationReason { get; set; }
    }
}
