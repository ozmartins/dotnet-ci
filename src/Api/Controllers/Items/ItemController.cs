using Swashbuckle.AspNetCore.Annotations;
using Api.Dtos.Items;
using Api.Infra;
using Api.Interfaces.Mappers;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Views.Items;
using Api.Controllers.Constants;

namespace Api.Controllers.Items
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        private readonly IItemMapper _itemMapper;

        public ItemController(IItemService itemService, IItemMapper itemMapper)
        {
            _itemService = itemService;
            _itemMapper = itemMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ItemView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ItemConstant.CreateSummary, Description = ItemConstant.CreateDescription, Tags = new[] { ItemConstant.Tag })]
        //TODO: O DTO deste método tem o ID do fornecedor, mas o ID do fornecedor deveria ser obtido a partir do usuário que consta no token de autenticação
        public IActionResult Create([FromBody] ItemDto dto)
        {
            try
            {
                var mapperResult = _itemMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _itemService.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _itemMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}/{version}")]
        [HttpPut]
        [ProducesResponseType(typeof(ItemView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ItemConstant.UpdateSummary, Description = ItemConstant.UpdateDescription, Tags = new[] { ItemConstant.Tag })]
        //TODO: O DTO deste método tem o ID do fornecedor, mas o ID do fornecedor não deveria ser editável.
        public IActionResult Update([FromRoute] Guid id, [FromRoute] Guid version, [FromBody] ItemDto dto)
        {
            try
            {
                var mapperResult = _itemMapper.Map(dto).DefineIdAndVersion(id, version);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _itemService.Update(id, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _itemMapper.Map(result.Entity);

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
        [SwaggerOperation(Summary = ItemConstant.DeleteSummary, Description = ItemConstant.DeleteDescription, Tags = new[] { ItemConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _itemService.Delete(id);

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
        [ProducesResponseType(typeof(ItemView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ItemConstant.GetByIdSummary, Description = ItemConstant.GetByIdDescription, Tags = new[] { ItemConstant.Tag })]

        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _itemService.Get(id);

                var view = _itemMapper.Map(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ItemView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ItemConstant.GetAllSummary, Description = ItemConstant.GetAllDescription, Tags = new[] { ItemConstant.Tag })]
        public IActionResult Get()
        {
            try
            {
                var entities = _itemService.Get();

                var view = _itemMapper.Map(entities);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
