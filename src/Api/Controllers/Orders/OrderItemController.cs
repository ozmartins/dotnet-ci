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
    [Route("order/{orderId}/item")]
    public class OrderItemController : Controller
    {
        private readonly IOrderMapper _orderMapper;

        private readonly IOrderItemMapper _orderItemMapper;

        private readonly IOrderService _orderService;

        public OrderItemController(IOrderMapper orderMapper, IOrderItemMapper orderItemMapper, IOrderService orderService)
        {
            _orderMapper = orderMapper;
            _orderItemMapper = orderItemMapper;
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderItemView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = OrderItemConstant.CreateSummary, Description = OrderItemConstant.CreateDescription, Tags = new[] { OrderItemConstant.Tag })]
        public IActionResult Create([FromRoute] Guid orderId, [FromBody] OrderItemDto dto)
        {
            try
            {
                var itemMapperResult = _orderItemMapper.Map(dto);

                if (!itemMapperResult.Success) return BadRequest(itemMapperResult.Errors);

                var result = _orderService.AddOrderItem(orderId, itemMapperResult.Entity);

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
        [HttpPut]
        [ProducesResponseType(typeof(OrderItemView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = OrderItemConstant.UpdateSummary, Description = OrderItemConstant.UpdateDescription, Tags = new[] { OrderItemConstant.Tag })]
        public IActionResult Update([FromRoute] Guid orderId, [FromRoute] Guid id, [FromBody] OrderItemDto dto)
        {
            try
            {
                var itemMapperResult = _orderItemMapper.Map(dto).DefineId(id);

                if (!itemMapperResult.Success) return BadRequest(itemMapperResult.Errors);

                var result = _orderService.ReplaceOrderItem(orderId, id, itemMapperResult.Entity);

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
        [SwaggerOperation(Summary = OrderItemConstant.DeleteSummary, Description = OrderItemConstant.DeleteDescription, Tags = new[] { OrderItemConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid orderId, [FromRoute] Guid id)
        {
            try
            {
                var result = _orderService.RemoveOrderItem(orderId, id);

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
        [HttpGet]
        [ProducesResponseType(typeof(OrderItemView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = OrderItemConstant.GetByIdSummary, Description = OrderItemConstant.GetByIdDescription, Tags = new[] { OrderItemConstant.Tag })]
        public IActionResult Get([FromRoute] Guid orderId, [FromRoute] Guid id)
        {
            try
            {
                var entity = _orderService.Get(orderId);

                var item = entity.Items.Find(x => x.Id.Equals(id));

                var view = _orderItemMapper.Map(item);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<OrderItemView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = OrderItemConstant.GetAllSummary, Description = OrderItemConstant.GetAllDescription, Tags = new[] { OrderItemConstant.Tag })]
        public IActionResult Get([FromRoute] Guid orderId)
        {
            try
            {
                var entity = _orderService.Get(orderId);

                var view = _orderItemMapper.Map(entity.Items);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
