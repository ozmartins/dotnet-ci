using Domain.Models.Reservation;

namespace Api.Dtos.Reservations
{
    public class ReservationDto
    {
        public DateTime Date { get; set; }
        public int InitialHour { get; set; }
        public int FinalHour { get; set; }
        public Guid ItemId { get; set; }
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
        public ReservationReason ReservationReason { get; set; }
    }
}
