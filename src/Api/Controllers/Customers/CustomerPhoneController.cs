using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using Api.Dtos.People;
using Api.Interfaces.Mappers;
using Domain.Interfaces.Services;
using Domain.Models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Views.People;
using Api.Controllers.Constants;

namespace Api.Controllers.Customers
{
    [Authorize]
    [ApiController]
    [Route("customer/{customerId}/phone")]
    public class CustomerPhoneController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        private readonly IMapper _autoMapper;

        private readonly ICustomerMapper _customerMapper;

        public CustomerPhoneController(ICustomerService customerService, IMapper autoMapper, ICustomerMapper customerMapper)
        {
            _customerService = customerService;
            _autoMapper = autoMapper;
            _customerMapper = customerMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PhoneView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CustomerPhoneConstant.CreateSummary, Description = CustomerPhoneConstant.CreateDescription, Tags = new[] { CustomerPhoneConstant.Tag })]
        public IActionResult Create([FromRoute] Guid customerId, [FromBody] PhoneDto dto)
        {
            try
            {
                var phone = _autoMapper.Map<Phone>(dto);

                var result = _customerService.AddPhone(customerId, phone);

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
        [SwaggerOperation(Summary = CustomerPhoneConstant.UpdateSummary, Description = CustomerPhoneConstant.UpdateDescription, Tags = new[] { CustomerPhoneConstant.Tag })]
        public IActionResult Update([FromRoute] Guid customerId, [FromRoute] Guid id, [FromBody] PhoneDto dto)
        {
            try
            {
                var phone = _autoMapper.Map<Phone>(dto);

                var result = _customerService.ReplacePhone(customerId, id, phone);

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
        [SwaggerOperation(Summary = CustomerPhoneConstant.DeleteSummary, Description = CustomerPhoneConstant.DeleteDescription, Tags = new[] { CustomerPhoneConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid customerId, [FromRoute] Guid id)
        {
            try
            {
                var result = _customerService.RemovePhone(customerId, id);

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
        [SwaggerOperation(Summary = CustomerPhoneConstant.GetByIdSummary, Description = CustomerPhoneConstant.GetByIdDescription, Tags = new[] { CustomerPhoneConstant.Tag })]
        public IActionResult Get([FromRoute] Guid customerId, [FromRoute] Guid id)
        {
            try
            {
                var customer = _customerService.Get(customerId);

                var phone = customer.Phones.Find(x => x.Id == id);

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
        [SwaggerOperation(Summary = CustomerPhoneConstant.GetAllSummary, Description = CustomerPhoneConstant.GetAllDescription, Tags = new[] { CustomerPhoneConstant.Tag })]
        public IActionResult Get([FromRoute] Guid customerId)
        {
            try
            {
                var customer = _customerService.Get(customerId);

                var phone = customer.Phones;

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
