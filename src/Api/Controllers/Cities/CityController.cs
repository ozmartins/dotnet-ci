using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using Api.Dtos.Cities;
using Api.Views.Cities;
using Domain.Interfaces;
using Domain.Interfaces.Validations;
using Domain.Models.Cities;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Controllers.Constants;

namespace Api.Controllers.Cities
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly BasicService<City> _cityService;

        private readonly IMapper _mapper;

        public CityController(IMapper mapper, IRepository<City> repository, ICityValidation validation)
        {
            _cityService = new BasicService<City>(repository, validation);
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CityView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CityConstant.CreateSummary, Description = CityConstant.CreateDescription, Tags = new[] { CityConstant.Tag })]
        //TODO: Esse método não deveria estar disponível na API pública
        public IActionResult Create([FromBody] CityDto dto)
        {
            try
            {
                var city = _mapper.Map<City>(dto);

                var result = _cityService.Create(city);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _mapper.Map<CityView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}/{version}")]
        [HttpPut]
        [ProducesResponseType(typeof(CityView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CityConstant.UpdateSummary, Description = CityConstant.UpdateDescription, Tags = new[] { CityConstant.Tag })]
        //TODO: Esse método não deveria estar disponível na API pública
        public IActionResult Update([FromRoute] Guid id, [FromRoute] Guid version, [FromBody] CityDto dto)
        {
            try
            {
                var city = _mapper.Map<City>(dto);

                city.DefineIdAndVersion(id, version);

                var result = _cityService.Update(id, city);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _mapper.Map<CityView>(result.Entity);

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
        [SwaggerOperation(Summary = CityConstant.DeleteSummary, Description = CityConstant.DeleteDescription, Tags = new[] { CityConstant.Tag })]
        //TODO: Esse método não deveria estar disponível na API pública
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _cityService.Delete(id);

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
        [ProducesResponseType(typeof(CityView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CityConstant.GetByIdSummary, Description = CityConstant.GetByIdDescription, Tags = new[] { CityConstant.Tag })]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _cityService.Get(id);

                var view = _mapper.Map<CityView>(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CityView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CityConstant.GetAllSummary, Description = CityConstant.GetAllDescription, Tags = new[] { CityConstant.Tag })]
        public IActionResult Get()
        {
            try
            {
                var entitys = _cityService.Get();

                var view = _mapper.Map<List<CityView>>(entitys);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
