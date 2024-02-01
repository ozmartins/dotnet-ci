using Api.Dtos;
using Api.Interfaces.Mappers;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Api.Views.PaymentPlans;
using AutoMapper;
using Api.Controllers.Constants;

namespace Api.Controllers.Suppliers
{
    [Authorize]
    [ApiController]
    [Route("supplier/{supplierId}/paymentplan")]
    public class SupplierPaymentPlanController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        private readonly ISupplierMapper _supplierMapper;

        private readonly IMapper _autoMapper;

        public SupplierPaymentPlanController(ISupplierService supplierService, ISupplierMapper supplierMapper, IMapper autoMapper)
        {
            _supplierService = supplierService;
            _supplierMapper = supplierMapper;
            _autoMapper = autoMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PaymentPlanView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierPaymentPlanConstant.CreateSummary, Description = SupplierPaymentPlanConstant.CreateDescription, Tags = new[] { SupplierPaymentPlanConstant.Tag })]
        public IActionResult Create([FromRoute] Guid supplierId, [FromBody] GuidDto dto)
        {
            try
            {
                var result = _supplierService.AddPaymentPlan(supplierId, dto.Id);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<PaymentPlanView>(result.Entity);

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
        [SwaggerOperation(Summary = SupplierPaymentPlanConstant.DeleteSummary, Description = SupplierPaymentPlanConstant.DeleteDescription, Tags = new[] { SupplierPaymentPlanConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid supplierId, [FromRoute] Guid id, [FromRoute] Guid version)
        {
            try
            {
                var result = _supplierService.RemovePaymentPlan(supplierId, id);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<PaymentPlanView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(PaymentPlanView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierPaymentPlanConstant.GetByIdSummary, Description = SupplierPaymentPlanConstant.GetByIdDescription, Tags = new[] { SupplierPaymentPlanConstant.Tag })]
        public IActionResult Get([FromRoute] Guid supplierId, [FromRoute] Guid id)
        {
            try
            {
                var supplier = _supplierService.Get(supplierId);

                var paymentPlan = supplier.SupplierInfo.PaymentPlans.Find(x => x.Id == id);

                var view = _autoMapper.Map<PaymentPlanView>(paymentPlan);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PaymentPlanView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierPaymentPlanConstant.GetAllSummary, Description = SupplierPaymentPlanConstant.GetAllDescription, Tags = new[] { SupplierPaymentPlanConstant.Tag })]
        public IActionResult Get([FromRoute] Guid supplierId)
        {
            try
            {
                var supplier = _supplierService.Get(supplierId);

                var paymentPlans = supplier.SupplierInfo.PaymentPlans;

                var view = _autoMapper.Map<List<PaymentPlanView>>(paymentPlans);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
