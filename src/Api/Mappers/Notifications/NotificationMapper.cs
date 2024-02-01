using Api.Dtos.Notifications;
using Api.Infra;
using Api.Interfaces.Mappers;
using Domain.Infra.Extensions;
using Domain.Models.Notications;
using Domain.Models.People;
using Domain.Interfaces;
using Domain.Models.Notifications;

namespace Api.Mappers.Notifications
{
    public class NotificationMapper : BaseMapper<Notification>, INotificationMapper
    {
        private IRepository<Person> _personRepository;

        public NotificationMapper(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public MapperResult<Notification> Map(NotificationDto dto)
        {
            var person = _personRepository.RecoverById(dto.DestinationId).IfNull(() => AddError("O destinatário da notificação não existe."));

            if (!SuccessResult()) return GetResult();

            SetEntity(new Notification(DateTime.Now, new PersonForNotification(person?.Id ?? Guid.Empty, person?.Name ?? string.Empty), dto.Text));

            return GetResult();
        }
    }
}
