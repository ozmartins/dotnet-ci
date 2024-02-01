using FluentValidation;
using Domain.Infra.Extensions;
using Domain.Interfaces.Filters;
using Domain.Interfaces.Validations;
using Domain.Models.Items;
using Domain.Models.Reservation;
using Domain.Interfaces;

namespace Domain.Validations
{
    public class ReservationValidation : AbstractValidator<Reservation>, IReservationValidation
    {
        public ReservationValidation(IRepository<Reservation> reservationRepository, IFilterBuilder<Reservation> reservationFilterBuilder)
        {
            RuleFor(x => x.Date).GreaterThan(DateTime.MinValue).WithMessage("A data da reserva não foi informada.");

            RuleFor(x => x.Date).GreaterThanOrEqualTo(DateTime.Today).WithMessage("A data da reserva precisa ser maior ou igual a data atual.");

            RuleFor(x => x.InitialHour).InclusiveBetween(0, 23).WithMessage("A hora inicial precisa estar entre 0 (meia-noite) e 23 horas.");

            RuleFor(x => x.InitialHour).LessThan(x => x.FinalHour).WithMessage("A hora inicial precisa ser menor que a hora final.");

            RuleFor(x => x.FinalHour).InclusiveBetween(0, 23).WithMessage("A hora final precisa estar entre 0 (meia-noite) e 23 horas.");

            RuleFor(x => x.ReservationReason).IsInEnum().WithMessage("O motivo da reserva informado é inválido.");

            RuleFor(x => orderWasInformed(x)).Equal(true).WithMessage("O pedido não foi informado.");

            RuleFor(x => itemHasAnOpenSchedule(x)).Equal(true).WithMessage("O item não possui agenda aberta para a data solicitada.");

            RuleFor(x => itemHasAvailableHours(reservationRepository, reservationFilterBuilder, x)).Equal(true).WithMessage("O item não possui disponibilidade nos horários solicitados.");
        }

        private bool orderWasInformed(Reservation reservation)
        {
            if (reservation.ReservationReason == ReservationReason.Order && reservation.OrderItem == null)
                return false;
            else
                return true;
        }
        private bool itemHasAnOpenSchedule(Reservation reservation)
        {
            return reservation.Item.Schedules.Where(x => x.DayOfWeek == reservation.Date.DayOfWeek).Count() > 0;
        }

        private bool itemHasAvailableHours(IRepository<Reservation> reservationRepository, IFilterBuilder<Reservation> reservationFilterBuilder, Reservation reservation)
        {
            var schedule = reservation.Item.Schedules.Where(x => x.DayOfWeek == reservation.Date.DayOfWeek).FirstOrDefault();

            if (schedule == null) return false;

            var scheduleHoursList = transformScheduleItemsIntoHoursList(schedule.Items);

            var reservations = getReservedHours(reservationRepository, reservationFilterBuilder, reservation);

            var availableHours = buildAvailableHoursList(scheduleHoursList, reservations);

            var wishedHoursList = buildWishedHoursList(reservation);

            foreach (var wishedHour in wishedHoursList)
            {
                if (!availableHours.Exists(x => x == wishedHour))
                {
                    return false;
                }
            }

            return true;
        }

        private List<int> buildWishedHoursList(Reservation reservation)
        {
            var result = new List<int>();

            for (int hour = reservation.InitialHour; hour < reservation.FinalHour; hour++)
            {
                result.Add(hour);
            }

            return result;
        }

        private List<int> buildAvailableHoursList(List<int> scheduleHoursList, List<Reservation> reservations)
        {
            foreach (var reservation in reservations)
            {
                for (int hour = reservation.InitialHour; hour < reservation.FinalHour; hour++)
                {
                    scheduleHoursList.RemoveAll(x => x == hour);
                }
            }

            return scheduleHoursList;
        }

        private List<int> transformScheduleItemsIntoHoursList(List<ScheduleItem> scheduleItems)
        {
            var result = new List<int>();

            foreach (var scheduleItem in scheduleItems)
            {
                for (int hour = scheduleItem.InitialHour; hour < scheduleItem.FinalHour; hour++)
                {
                    result.Add(hour);
                }
            }

            return result;
        }

        private List<Reservation> getReservedHours(IRepository<Reservation> reservationRepository, IFilterBuilder<Reservation> reservationFilterBuilder, Reservation reservation)
        {
            reservationFilterBuilder
                .Equal(x => x.Item, reservation.Item)
                .GreaterThan(x => x.Date, reservation.Date.Date)
                .LessThan(x => x.Date, reservation.Date.EndOfDay());

            return reservationRepository.Recover(reservationFilterBuilder);
        }
    }
}
