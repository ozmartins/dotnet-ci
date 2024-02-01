using FluentValidation;
using Domain.Models.Reservation;

namespace Domain.Interfaces.Validations
{
    public interface IReservationValidation : IValidator<Reservation>
    {
    }
}
