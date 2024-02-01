using Api.Controllers.Constants;
using Api.Dtos.Addresses;
using Api.Infra;
using Api.Interfaces.Mappers;
using Api.Views.Addresses;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers.Suppliers
{
    [Authorize]
    [ApiController]
    [Route("supplier/{supplierId}/address")]
    public class SupplierAddressController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        private readonly IAddressMapper _addressMapper;

        public SupplierAddressController(ISupplierService supplierService, IAddressMapper addressMapper)
        {
            _supplierService = supplierService;
            _addressMapper = addressMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddressView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierAddressConstant.CreateSummary, Description = SupplierAddressConstant.CreateDescription, Tags = new[] { SupplierAddressConstant.Tag })]
        public IActionResult Create([FromRoute] Guid supplierId, [FromBody] AddressDto dto)
        {
            try
            {
                var mapperResult = _addressMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _supplierService.AddAddress(supplierId, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _addressMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(typeof(AddressView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierAddressConstant.UpdateSummary, Description = SupplierAddressConstant.UpdateDescription, Tags = new[] { SupplierAddressConstant.Tag })]
        public IActionResult Update([FromRoute] Guid supplierId, [FromRoute] Guid id, [FromBody] AddressDto dto)
        {
            try
            {
                var mapperResult = _addressMapper.Map(dto).DefineId(id);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _supplierService.ReplaceAddress(supplierId, id, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _addressMapper.Map(result.Entity);

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
        [SwaggerOperation(Summary = SupplierAddressConstant.DeleteSummary, Description = SupplierAddressConstant.DeleteDescription, Tags = new[] { SupplierAddressConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid supplierId, [FromRoute] Guid id)
        {
            try
            {
                var result = _supplierService.RemoveAddress(supplierId, id);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _addressMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(AddressView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierAddressConstant.GetByIdSummary, Description = SupplierAddressConstant.GetByIdDescription, Tags = new[] { SupplierAddressConstant.Tag })]
        public IActionResult Get([FromRoute] Guid supplierId, [FromRoute] Guid id)
        {
            try
            {
                var supplier = _supplierService.Get(supplierId);

                var address = supplier.Addresses.Find(x => x.Id == id);

                var view = _addressMapper.Map(address);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AddressView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierAddressConstant.GetAllSummary, Description = SupplierAddressConstant.GetAllDescription, Tags = new[] { SupplierAddressConstant.Tag })]
        public IActionResult Get([FromRoute] Guid supplierId)
        {
            try
            {
                var supplier = _supplierService.Get(supplierId);

                var address = supplier.Addresses;

                var view = _addressMapper.Map(address);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
