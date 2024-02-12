using FluentValidation;
using Domain.Interfaces.Validations;
using Domain.Models.Items;
using System.Linq;

namespace Domain.Validations
{
    public class ItemScheduleValidation : AbstractValidator<Item>, IItemScheduleValidation
    {
        public ItemScheduleValidation()
        {
            RuleFor(p => scheduleDuplicated(p)).Equal(false).WithMessage("O item possui mais de uma agenda aberta para o mesmo dia da semana.");

            RuleFor(p => ScheduleItemInConflict(p)).Equal(false).WithMessage("O item possui um ou mais dias da semana com horários conflitantes.");
        }

        private static bool scheduleDuplicated(Item item)
        {
            var result = item.Schedules
                .GroupBy(x => x.DayOfWeek)
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);

            return result.Any();
        }

        private static bool ScheduleItemInConflict(Item item)
        {
            var ids = from schedule in item.Schedules
                      from currentScheduleItem in schedule.Items
                      let equalHours = schedule
                                    .Items
                                    .Where(otherScheduleItem =>
                                        otherScheduleItem.Id != currentScheduleItem.Id &&
                                        otherScheduleItem.InitialHour == currentScheduleItem.InitialHour &&
                                        otherScheduleItem.FinalHour == currentScheduleItem.FinalHour)
                      let hoursWhereInitialHourItIsInsideTheRange = schedule
                            .Items
                            .Where(otherScheduleItem =>
                                otherScheduleItem.Id != currentScheduleItem.Id &&
                                otherScheduleItem.InitialHour < currentScheduleItem.InitialHour &&
                                currentScheduleItem.InitialHour < otherScheduleItem.FinalHour)
                      let hoursWhereFinalHourItIsInsideTheRange = schedule
                            .Items
                            .Where(otherScheduleItem =>
                                otherScheduleItem.Id != currentScheduleItem.Id &&
                                otherScheduleItem.InitialHour < currentScheduleItem.FinalHour &&
                                currentScheduleItem.FinalHour < otherScheduleItem.FinalHour)
                      where equalHours.Any() || hoursWhereInitialHourItIsInsideTheRange.Any() || hoursWhereFinalHourItIsInsideTheRange.Any()
                      select new { schedule.Id };

            return ids.Any();
        }
    }
}
