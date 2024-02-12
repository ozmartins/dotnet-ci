using Swashbuckle.AspNetCore.Annotations;
using Api.Dtos.Orders;
using Api.Infra;
using Api.Interfaces.Mapppers;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Views.Orders;
using Api.Controllers.Constants;

namespace Api.Controllers.Orders
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderMapper _orderMapper;

        private readonly IOrderService _orderService;

        public OrderController(IOrderMapper orderMapper, IOrderService orderService)
        {
            _orderMapper = orderMapper;
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = OrderConstant.CreateSummary, Description = OrderConstant.CreateDescription, Tags = new[] { OrderConstant.Tag })]
        public IActionResult Create([FromBody] OrderDto dto)
        {
            try
            {
                var mapperResult = _orderMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _orderService.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _orderMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}/{version}")]
        [HttpPut]
        [ProducesResponseType(typeof(OrderView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = OrderConstant.UpdateSummary, Description = OrderConstant.UpdateDescription, Tags = new[] { OrderConstant.Tag })]
        public IActionResult Update([FromRoute] Guid id, [FromRoute] Guid version, [FromBody] OrderDto dto)
        {
            try
            {
                var mapperResult = _orderMapper.Map(dto).DefineIdAndVersion(id, version);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _orderService.Update(id, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _orderMapper.Map(result.Entity);

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
        [SwaggerOperation(Summary = OrderConstant.DeleteSummary, Description = OrderConstant.DeleteDescription, Tags = new[] { OrderConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _orderService.Delete(id);

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
        [ProducesResponseType(typeof(OrderView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = OrderConstant.GetByIdSummary, Description = OrderConstant.GetByIdDescription, Tags = new[] { OrderConstant.Tag })]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _orderService.Get(id);

                var view = _orderMapper.Map(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<OrderView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = OrderConstant.GetAllSummary, Description = OrderConstant.GetAllDescription, Tags = new[] { OrderConstant.Tag })]
        public IActionResult Get()
        {
            try
            {
                var entities = _orderService.Get();

                var view = _orderMapper.Map(entities);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
