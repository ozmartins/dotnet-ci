using Api.Dtos.Reservations;
using Api.Infra;
using Api.Views.Reservations;
using Domain.Models.Reservation;

namespace Api.Interfaces.Mappers
{
    public interface IReservationMapper
    {
        MapperResult<Reservation> Map(ReservationDto dto);

        ReservationView? Map(Reservation? entity);

        List<ReservationView> Map(List<Reservation> entities);
    }
}
