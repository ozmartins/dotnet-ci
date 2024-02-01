using Api.Controllers.Constants;
using Api.Dtos.People;
using Api.Infra;
using Api.Interfaces.Mappers;
using Api.Views.People;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers.Suppliers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        private readonly ISupplierMapper _supplierMapper;

        public SupplierController(ISupplierService supplierService, ISupplierMapper supplierMapper)
        {
            _supplierService = supplierService;
            _supplierMapper = supplierMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SupplierView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierConstant.CreateSummary, Description = SupplierConstant.CreateDescription, Tags = new[] { SupplierConstant.Tag })]
        public IActionResult Create([FromBody] SupplierDto dto)
        {
            try
            {
                var mapperResult = _supplierMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _supplierService.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _supplierMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}/{version}")]
        [HttpPut]
        [ProducesResponseType(typeof(SupplierView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierConstant.UpdateSummary, Description = SupplierConstant.UpdateDescription, Tags = new[] { SupplierConstant.Tag })]
        public IActionResult Update([FromRoute] Guid id, [FromRoute] Guid version, [FromBody] SupplierDto dto)
        {
            try
            {
                var mapperResult = _supplierMapper.Map(dto).DefineIdAndVersion(id, version);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _supplierService.Update(id, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _supplierMapper.Map(result.Entity);

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
        [SwaggerOperation(Summary = SupplierConstant.DeleteSummary, Description = SupplierConstant.DeleteDescription, Tags = new[] { SupplierConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _supplierService.Delete(id);

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
        [ProducesResponseType(typeof(SupplierView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierConstant.GetByIdSummary, Description = SupplierConstant.GetByIdDescription, Tags = new[] { SupplierConstant.Tag })]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _supplierService.Get(id);

                var view = _supplierMapper.Map(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<SupplierView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierConstant.GetAllSummary, Description = SupplierConstant.GetAllDescription, Tags = new[] { SupplierConstant.Tag })]
        public IActionResult Get()
        {
            try
            {
                var entities = _supplierService.Get();

                var view = _supplierMapper.Map(entities);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
