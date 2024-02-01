using Swashbuckle.AspNetCore.Annotations;
using Api.Dtos.People;
using Api.Infra;
using Api.Interfaces.Mappers;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Views.People;
using Api.Controllers.Constants;

namespace Api.Controllers.Customers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        private readonly ICustomerMapper _customerMapper;

        public CustomerController(ICustomerService customerService, ICustomerMapper customerMapper)
        {
            _customerService = customerService;
            _customerMapper = customerMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CustomerConstant.CreateSummary, Description = CustomerConstant.CreateDescription, Tags = new[] { CustomerConstant.Tag })]
        //TODO: Esse método não seria público, pois um cliente deve ser adicionado internamente quando um usuário fosse criado.
        public IActionResult Create([FromBody] CustomerDto dto)
        {
            try
            {
                var mapperResult = _customerMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _customerService.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _customerMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}/{version}")]
        [HttpPut]
        [ProducesResponseType(typeof(CustomerView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CustomerConstant.UpdateSummary, Description = CustomerConstant.UpdateDescription, Tags = new[] { CustomerConstant.Tag })]
        public IActionResult Update([FromRoute] Guid id, [FromRoute] Guid version, [FromBody] CustomerDto dto)
        {
            try
            {
                var mapperResult = _customerMapper.Map(dto).DefineIdAndVersion(id, version);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _customerService.Update(id, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _customerMapper.Map(result.Entity);

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
        [SwaggerOperation(Summary = CustomerConstant.DeleteSummary, Description = CustomerConstant.DeleteDescription, Tags = new[] { CustomerConstant.Tag })]
        //TODO: Esse método não seria público, pois um cliente deveria ser removido internamente quando uma conta de usuário fosse removida.
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _customerService.Delete(id);

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
        [ProducesResponseType(typeof(CustomerView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CustomerConstant.GetByIdSummary, Description = CustomerConstant.GetByIdDescription, Tags = new[] { CustomerConstant.Tag })]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _customerService.Get(id);

                var view = _customerMapper.Map(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CustomerConstant.GetAllSummary, Description = CustomerConstant.GetAllDescription, Tags = new[] { CustomerConstant.Tag })]
        //TODO: Não consigo ver um cenário em que esse método deva ser usado.
        public IActionResult Get()
        {
            try
            {
                var entities = _customerService.Get();

                var view = _customerMapper.Map(entities);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
