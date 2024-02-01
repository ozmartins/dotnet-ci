using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using Api.Dtos.Notifications;
using Api.Interfaces.Mappers;
using Api.Views.Notifications;
using Domain.Interfaces;
using Domain.Interfaces.Validations;
using Domain.Models.Notications;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Controllers.Constants;

namespace Api.Controllers.Notifications
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly BasicService<Notification> _notificationService;
        private readonly IMapper _autoMapper;
        private readonly INotificationMapper _notificationMapper;
        public NotificationController(IMapper autoMapper,
                                      INotificationMapper notificationMapper,
                                      IRepository<Notification> repository,
                                      INotificationValidation validation)
        {
            _notificationService = new BasicService<Notification>(repository, validation);
            _autoMapper = autoMapper;
            _notificationMapper = notificationMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(NotificationView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = NotificationConstant.CreateSummary, Description = NotificationConstant.CreateDescription, Tags = new[] { NotificationConstant.Tag })]
        public IActionResult Create([FromBody] NotificationDto dto)
        {
            try
            {
                var mapperResult = _notificationMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _notificationService.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<NotificationView>(result.Entity);

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
        [SwaggerOperation(Summary = NotificationConstant.DeleteSummary, Description = NotificationConstant.DeleteDescription, Tags = new[] { NotificationConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _notificationService.Delete(id);

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
        [ProducesResponseType(typeof(NotificationView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = NotificationConstant.GetByIdSummary, Description = NotificationConstant.GetByIdDescription, Tags = new[] { NotificationConstant.Tag })]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _notificationService.Get(id);

                var view = _autoMapper.Map<NotificationView>(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<NotificationView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = NotificationConstant.GetAllSummary, Description = NotificationConstant.GetAllDescription, Tags = new[] { NotificationConstant.Tag })]
        public IActionResult Get()
        {
            try
            {
                var entitys = _notificationService.Get();

                var view = _autoMapper.Map<List<NotificationView>>(entitys);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
