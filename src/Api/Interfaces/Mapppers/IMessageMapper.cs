using Api.Dtos.Messages;
using Api.Infra;
using Domain.Models.Messages;

namespace Api.Interfaces.Mappers
{
    public interface IMessageMapper
    {
        MapperResult<Message> Map(MessageDto dto);
    }
}
