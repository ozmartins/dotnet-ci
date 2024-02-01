using Api.Dtos.Reviews;
using Api.Infra;
using Api.Interfaces.Mapppers;
using Domain.Interfaces;
using Domain.Interfaces.Validations;
using Domain.Models.Review;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Api.Views.Reviews;
using Api.Controllers.Constants;

namespace Api.Controllers.Reviews
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewMapper _reviewMapper;
        private readonly BasicService<Review> _reviewService;

        public ReviewController(IReviewMapper reviewMapper, IRepository<Review> repository, IReviewValidation validation)
        {
            _reviewMapper = reviewMapper;
            _reviewService = new BasicService<Review>(repository, validation);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReviewView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ReviewConstant.CreateSummary, Description = ReviewConstant.CreateDescription, Tags = new[] { ReviewConstant.Tag })]
        public IActionResult Create([FromBody] ReviewDto dto)
        {
            try
            {
                var mapperResult = _reviewMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _reviewService.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _reviewMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}/{version}")]
        [HttpPut]
        [ProducesResponseType(typeof(ReviewView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ReviewConstant.UpdateSummary, Description = ReviewConstant.UpdateDescription, Tags = new[] { ReviewConstant.Tag })]
        public IActionResult Update([FromRoute] Guid id, [FromRoute] Guid version, [FromBody] ReviewDto dto)
        {
            try
            {
                var mapperResult = _reviewMapper.Map(dto).DefineIdAndVersion(id, version);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _reviewService.Update(id, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _reviewMapper.Map(result.Entity);

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
        [SwaggerOperation(Summary = ReviewConstant.DeleteSummary, Description = ReviewConstant.DeleteDescription, Tags = new[] { ReviewConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _reviewService.Delete(id);

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
        [ProducesResponseType(typeof(ReviewView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ReviewConstant.GetByIdSummary, Description = ReviewConstant.GetByIdDescription, Tags = new[] { ReviewConstant.Tag })]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _reviewService.Get(id);

                var view = _reviewMapper.Map(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ReviewView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ReviewConstant.GetAllSummary, Description = ReviewConstant.GetAllDescription, Tags = new[] { ReviewConstant.Tag })]
        public IActionResult Get()
        {
            try
            {
                var entities = _reviewService.Get();

                var view = _reviewMapper.Map(entities);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
