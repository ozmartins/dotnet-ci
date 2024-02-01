using AutoMapper;
using Api.Controllers.Constants;
using Api.Dtos.People;
using Api.Interfaces.Mappers;
using Api.Views.People;
using Domain.Interfaces.Services;
using Domain.Models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers.Suppliers
{
    [Authorize]
    [ApiController]
    [Route("supplier/{supplierId}/phone")]
    public class SupplierPhoneController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        private readonly IMapper _autoMapper;

        private readonly ISupplierMapper _supplierMapper;

        public SupplierPhoneController(ISupplierService supplierService, IMapper autoMapper, ISupplierMapper supplierMapper)
        {
            _supplierService = supplierService;
            _autoMapper = autoMapper;
            _supplierMapper = supplierMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PhoneView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierPhoneConstant.CreateSummary, Description = SupplierPhoneConstant.CreateDescription, Tags = new[] { SupplierPhoneConstant.Tag })]
        public IActionResult Create([FromRoute] Guid supplierId, [FromBody] PhoneDto dto)
        {
            try
            {
                var phone = _autoMapper.Map<Phone>(dto);

                var result = _supplierService.AddPhone(supplierId, phone);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<PhoneView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(typeof(PhoneView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierPhoneConstant.UpdateSummary, Description = SupplierPhoneConstant.UpdateDescription, Tags = new[] { SupplierPhoneConstant.Tag })]
        public IActionResult Update([FromRoute] Guid supplierId, [FromRoute] Guid id, [FromBody] PhoneDto dto)
        {
            try
            {
                var phone = _autoMapper.Map<Phone>(dto);

                var result = _supplierService.ReplacePhone(supplierId, id, phone);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<PhoneView>(result.Entity);

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
        [SwaggerOperation(Summary = SupplierPhoneConstant.DeleteSummary, Description = SupplierPhoneConstant.DeleteDescription, Tags = new[] { SupplierPhoneConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid supplierId, [FromRoute] Guid id)
        {
            try
            {
                var result = _supplierService.RemovePhone(supplierId, id);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<PhoneView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(PhoneView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierPhoneConstant.GetByIdSummary, Description = SupplierPhoneConstant.GetByIdDescription, Tags = new[] { SupplierPhoneConstant.Tag })]
        public IActionResult Get([FromRoute] Guid supplierId, [FromRoute] Guid id)
        {
            try
            {
                var supplier = _supplierService.Get(supplierId);

                var phone = supplier.Phones.Find(x => x.Id == id);

                var view = _autoMapper.Map<PhoneView>(phone);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PhoneView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierPhoneConstant.GetAllSummary, Description = SupplierPhoneConstant.GetAllDescription, Tags = new[] { SupplierPhoneConstant.Tag })]
        public IActionResult Get([FromRoute] Guid supplierId)
        {
            try
            {
                var supplier = _supplierService.Get(supplierId);

                var phone = supplier.Phones;

                var view = _autoMapper.Map<List<PhoneView>>(phone);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
