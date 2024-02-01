using Swashbuckle.AspNetCore.Annotations;
using Api.Dtos.InventoryStatements;
using Api.Interfaces.Mappers;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Views.InventoryStatements;
using Api.Controllers.Constants;

namespace Api.Controllers.InventoryStatements
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class InventoryStatementController : ControllerBase
    {
        private readonly IInventoryStatementService _serviceInventoryStatement;

        private readonly IInventoryStatementMapper _inventoryStatementMapper;

        public InventoryStatementController(IInventoryStatementService serviceInventoryStatement, IInventoryStatementMapper inventoryStatementMapper)
        {
            _serviceInventoryStatement = serviceInventoryStatement;
            _inventoryStatementMapper = inventoryStatementMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(InventoryStatementView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = InventoryStatementConstant.CreateSummary, Description = InventoryStatementConstant.CreateDescription, Tags = new[] { InventoryStatementConstant.Tag })]
        //TODO: Esse método não deve ser público, pois uma entrada de extrato de estoque deve ser criada quando um pedido é enviado.
        public IActionResult Create([FromBody] InventoryStatementDto dto)
        {
            try
            {
                var mapperResult = _inventoryStatementMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _serviceInventoryStatement.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _inventoryStatementMapper.Map(result.Entity);

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
        [SwaggerOperation(Summary = InventoryStatementConstant.DeleteSummary, Description = InventoryStatementConstant.DeleteDescription, Tags = new[] { InventoryStatementConstant.Tag })]
        //TODO: Esse método não deve ser público, pois uma entrada de extrato de estoque deve ser removida quando o envio de um pedido é cancelado.
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _serviceInventoryStatement.Delete(id);

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
        [ProducesResponseType(typeof(InventoryStatementView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = InventoryStatementConstant.GetByIdSummary, Description = InventoryStatementConstant.GetByIdDescription, Tags = new[] { InventoryStatementConstant.Tag })]
        //TODO: Não consigo ver um cenário em que esse método será usado.
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _serviceInventoryStatement.Get(id);

                var view = _inventoryStatementMapper.Map(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<InventoryStatementView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = InventoryStatementConstant.GetAllSummary, Description = InventoryStatementConstant.GetAllDescription, Tags = new[] { InventoryStatementConstant.Tag })]
        public IActionResult Get()
        {
            try
            {
                var entitys = _serviceInventoryStatement.Get();

                var view = _inventoryStatementMapper.Map(entitys);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
