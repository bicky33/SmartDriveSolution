using Contract.DTO.CR.Request;
using Contract.DTO.CR.Response;
using Contract.DTO.SO;
using Domain.Entities.CR;
using Domain.Exceptions;
using Domain.RequestFeatured;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.CR;
using Service.Abstraction.SO;
using Service.CR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.CR
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerRequestController : ControllerBase
    {
        private readonly IServiceCustomerManager _serviceCustomerManager;
        private readonly IServiceSOManager _serviceSOManager;

        public CustomerRequestController(IServiceCustomerManager serviceCustomerManager, IServiceSOManager serviceSOManager)
        {
            _serviceCustomerManager = serviceCustomerManager;
            _serviceSOManager = serviceSOManager;
        }

        // GET: api/<CustomerRequestController>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var customerRequestDto = await _serviceCustomerManager.CustomerRequestService.GetAllAsync(false);
            return Ok(customerRequestDto);
        }

        [HttpGet("request")]
        public async Task<ActionResult> GetAllByUserOrEmployee(int userentityid, string arwgCode, string role)
        {
            if (role == "Customer")
            {
                var customerRequest = await _serviceCustomerManager.CustomerRequestService.GetAllByCustomer(userentityid, false);
                return Ok(customerRequest);
            }
            else if (role == "Employee")
            {
                var customerRequest = await _serviceCustomerManager.CustomerRequestService.GetAllByEmployee(arwgCode, false);
                return Ok(customerRequest);
            }

            return NoContent();
        }

        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<CustomerRequestDto>>> GetCustomerRequestPaging([FromQuery] EntityParameter entityParameter)
        {
            var customerRequestDto = await _serviceCustomerManager.CustomerRequestService.GetAllPagingAsync(entityParameter, false);
            return Ok(customerRequestDto);
        }

        // GET api/<CustomerRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerRequestDto>> GetById(int id)
        {
            var customerRequestDto = await _serviceCustomerManager.CustomerRequestService.GetByIdAsync(id, false);
            return Ok(customerRequestDto);
        }

        // POST api/<CustomerRequestController>
        [HttpPost("request/create/agen")]
        public async Task<IActionResult> CreateRequestByAgen([FromBody] CreateRequestByAgenDto request)
        {
            await _serviceCustomerManager.CustomerInscAssetService.ValidatePoliceNumber(request.CustomerInscAsset.CiasPoliceNumber);

            var totalPremi = await _serviceCustomerManager.CustomerInscAssetService.GetPremiPrice(request.CustomerInscAsset.CiasIntyName, (int)request.CustomerInscAsset.CiasCarsId, (int)request.CustomerInscAsset.CiasCityId, request.CustomerInscAsset.CiasCurrentPrice);
            request.CustomerInscAsset.CiasTotalPremi = totalPremi;

            var customerRequest = await _serviceCustomerManager.CustomerRequestService.CreateRequestByAgen(request);

            var createServicePolisFeasibilityDto = new CreateServicePolisFeasibilityDto()
            {
                CreqId = customerRequest.CreqEntityid,
                CustId = (int)customerRequest.CreqCustEntityid,
                AgentId = (int)customerRequest.CreqAgenEntityid,
                ServVehicleNo = customerRequest.CustomerInscAsset.CiasPoliceNumber,
                CreatePolisDate = (DateTime)customerRequest.CreqCreateDate,
                PolisStartDate = customerRequest.CustomerInscAsset.CiasStartdate,
                PolisEndDate = customerRequest.CustomerInscAsset.CiasEnddate
            };

            await _serviceSOManager.ServiceService.CreateServiceFeasibility(createServicePolisFeasibilityDto);

            return CreatedAtAction(nameof(GetById), new { id = customerRequest.CreqEntityid }, customerRequest);
        }

        [HttpPost("request/create/customer")]
        public async Task<IActionResult> CreateRequestByCustomer([FromBody] CreateRequestByCustomerDto request)
        {
            await _serviceCustomerManager.CustomerInscAssetService.ValidatePoliceNumber(request.CustomerInscAsset.CiasPoliceNumber);

            var totalPremi = await _serviceCustomerManager.CustomerInscAssetService.GetPremiPrice(request.CustomerInscAsset.CiasIntyName, (int)request.CustomerInscAsset.CiasCarsId, (int)request.CustomerInscAsset.CiasCityId, request.CustomerInscAsset.CiasCurrentPrice);
            request.CustomerInscAsset.CiasTotalPremi = totalPremi;

            var customerRequest = await _serviceCustomerManager.CustomerRequestService.CreateRequestByCustomer(request);

            var createServicePolisFeasibilityDto = new CreateServicePolisFeasibilityDto()
            {
                CreqId = customerRequest.CreqEntityid,
                CustId = (int)customerRequest.CreqCustEntityid,
                AgentId = (int)customerRequest.CreqAgenEntityid,
                ServVehicleNo = customerRequest.CustomerInscAsset.CiasPoliceNumber,
                CreatePolisDate = (DateTime)customerRequest.CreqCreateDate,
                PolisStartDate = customerRequest.CustomerInscAsset.CiasStartdate,
                PolisEndDate = customerRequest.CustomerInscAsset.CiasEnddate
            };

            await _serviceSOManager.ServiceService.CreateServiceFeasibility(createServicePolisFeasibilityDto);

            return CreatedAtAction(nameof(GetById), new { id = customerRequest.CreqEntityid }, customerRequest);
        }

        [HttpPut("request/polis")]
        public async Task<IActionResult> RequestNewPolis([FromBody] CustomerPolisRequestDto request)
        {
            try
            {
                var newPolis = await _serviceCustomerManager.CustomerRequestService.CreatePolis(request);

                var createServicePolisDto = new CreateServicePolisDto()
                {
                    ServId = newPolis.Servs[0].ServId,
                    AgentId = (int)newPolis.CreqAgenEntityid,
                    CreatePolisDate = (DateTime)newPolis.CreqModifiedDate,
                    PolisStartDate = newPolis.CustomerInscAsset.CiasStartdate,
                    PolisEndDate = newPolis.CustomerInscAsset.CiasEnddate                    
                };

                await _serviceSOManager.ServiceService.CreateServicePolis(createServicePolisDto);

                return Ok(newPolis);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("request/claim")]
        public async Task<IActionResult> RequestClaimPolis([FromBody] CustomerClaimRequestDto customerClaimDto)
        {
            try
            {
                var claimedRequest = await _serviceCustomerManager.CustomerClaimService.ClaimPolis(customerClaimDto);

                return Ok(claimedRequest);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("request/close")]
        public async Task<IActionResult> RequestClosePolis([FromBody] CustomerCloseRequestDto customerCloseDto)
        {
            try
            {
                var closedRequest = await _serviceCustomerManager.CustomerClaimService.ClosePolis(customerCloseDto);
                return Ok(closedRequest);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT api/<CustomerRequestController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerRequestUpdateDto customerRequestDto)
        {
            await _serviceCustomerManager.CustomerRequestService.UpdateAsync(id, customerRequestDto.Adapt<CustomerRequestDto>());
            return NoContent();
        }

        // DELETE api/<CustomerRequestController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerRequest(int id)
        {
            await _serviceCustomerManager.CustomerClaimService.DeleteAsync(id);
            await _serviceCustomerManager.CustomerInscAssetService.DeleteAsync(id);
            await _serviceCustomerManager.CustomerRequestService.DeleteAsync(id);
            return NoContent();
        }
    }
}