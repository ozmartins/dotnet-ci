using Api.Dtos.Notifications;
using Api.Infra;
using Domain.Models.Notications;

namespace Api.Interfaces.Mappers
{
    public interface INotificationMapper
    {
        MapperResult<Notification> Map(NotificationDto dto);
    }
}
