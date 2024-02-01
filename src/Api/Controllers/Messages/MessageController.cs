using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using Api.Dtos.Messages;
using Api.Infra;
using Api.Interfaces.Mappers;
using Api.Views.Messages;
using Domain.Interfaces;
using Domain.Interfaces.Validations;
using Domain.Models.Messages;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Controllers.Constants;

namespace Api.Controllers.Messages
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private BasicService<Message> _serviceMessage;

        private readonly IMapper _autoMapper;

        private readonly IMessageMapper _messageMapper;

        public MessageController(IMapper autoMapper, IMessageMapper messageMapper, IRepository<Message> repository, IMessageValidation validation)
        {
            _serviceMessage = new BasicService<Message>(repository, validation);
            _autoMapper = autoMapper;
            _messageMapper = messageMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(MessageView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = MessageConstant.CreateSummary, Description = MessageConstant.CreateDescription, Tags = new[] { MessageConstant.Tag })]
        public IActionResult Create([FromBody] MessageDto dto)
        {
            try
            {
                var mapperResult = _messageMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _serviceMessage.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<MessageView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}/{version}")]
        [HttpPut]
        [ProducesResponseType(typeof(MessageView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = MessageConstant.UpdateSummary, Description = MessageConstant.UpdateDescription, Tags = new[] { MessageConstant.Tag })]
        public IActionResult Update([FromRoute] Guid id, [FromRoute] Guid version, [FromBody] MessageDto dto)
        {
            try
            {
                var mapperResult = _messageMapper.Map(dto).DefineIdAndVersion(id, version);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _serviceMessage.Update(id, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<MessageView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = MessageConstant.DeleteSummary, Description = MessageConstant.DeleteDescription, Tags = new[] { MessageConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _serviceMessage.Delete(id);

                if (!result.Success) return BadRequest(result.Errors);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(MessageView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = MessageConstant.GetByIdSummary, Description = MessageConstant.GetByIdDescription, Tags = new[] { MessageConstant.Tag })]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _serviceMessage.Get(id);

                var view = _autoMapper.Map<MessageView>(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<MessageView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = MessageConstant.GetAllSummary, Description = MessageConstant.GetAllDescription, Tags = new[] { MessageConstant.Tag })]
        public IActionResult Get()
        {
            try
            {
                var entitys = _serviceMessage.Get();

                var view = _autoMapper.Map<List<MessageView>>(entitys);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
