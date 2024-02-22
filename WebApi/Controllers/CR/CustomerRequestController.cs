﻿using Contract.DTO.CR.Request;
using Contract.DTO.CR.Response;
using Contract.DTO.SO;
using Domain.Exceptions;
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
        private readonly IServiceRequestSOManager _serviceRequestSOManager;

        public CustomerRequestController(IServiceCustomerManager serviceCustomerManager, IServiceRequestSOManager serviceRequestSOManager)
        {
            _serviceCustomerManager = serviceCustomerManager;
            _serviceRequestSOManager = serviceRequestSOManager;
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
                var customerRequest = await _serviceCustomerManager.CustomerRequestService.GetAllByUser(userentityid, false);
                return Ok(customerRequest);

            }
            else if (role == "Employee")
            {
                var customerRequest = await _serviceCustomerManager.CustomerRequestService.GetAllByEmployee(arwgCode, false);
                return Ok(customerRequest);
            }

            return NoContent();
        }


        // GET api/<CustomerRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerRequestResponseDto>> GetById(int id)
        {
            var customerRequestDto = await _serviceCustomerManager.CustomerRequestService.GetByIdAsync(id, false);
            return Ok(customerRequestDto);
        }

        // POST api/<CustomerRequestController>    
        [HttpPost("request")]
        public async Task<IActionResult> CreateCustomerRequest([FromBody] CreateCustomerRequestDto request)
        {
            await _serviceCustomerManager.CustomerInscAssetService.ValidatePoliceNumber(request.CustomerInscAsset.CiasPoliceNumber);

            var totalPremi = await _serviceCustomerManager.CustomerInscAssetService.GetPremiPrice(request.CustomerInscAsset.CiasIntyName, request.CustomerInscAsset.CiasCarsId, request.CustomerInscAsset.CiasCityId, request.CustomerInscAsset.CiasCurrentPrice);
            request.CustomerInscAsset.CiasTotalPremi = totalPremi;

            var customerRequest = await _serviceCustomerManager.CustomerRequestService.CreateRequest(request);

            return CreatedAtAction(nameof(GetById), new { id = customerRequest.CreqEntityid }, customerRequest);
        }

        [HttpPost("request/create/user")]
        public async Task<IActionResult> CreateRequestByUser([FromBody] CustomerRequestRequestDto customerRequestDto)
        {
            var customerRequest = await _serviceCustomerManager.CustomerRequestService.CreateByUser(customerRequestDto);

            var createServicePolisDto = new CreateServicePolisFeasibilityDto()
            {
                CreqId = customerRequest.CreqEntityid,
                CustId = customerRequest.CreqCustEntityid,
                AgentId = customerRequest.CreqAgenEntityid,
                CreatePolisDate = customerRequest.CreqCreateDate,
                PolisStartDate = customerRequest.CustomerInscAsset.CiasStartdate,
                PolisEndDate = customerRequest.CustomerInscAsset.CiasEnddate
            };

            await _serviceRequestSOManager.ServiceRequest.CreateServicePolisFeasibility(createServicePolisDto);

            return CreatedAtAction(nameof(GetById), new { id = customerRequest.CreqEntityid }, customerRequest);
        }

        [HttpPost("request/create/agen")]
        public async Task<IActionResult> CreateRequestByEmployee([FromBody] CreateCustomerRequestByAgenDto customerRequestDto)
        {
            var customerRequest = await _serviceCustomerManager.CustomerRequestService.CreateByEmployee(customerRequestDto);

            var createServicePolisDto = new CreateServicePolisFeasibilityDto()
            {
                CreqId = customerRequest.CreqEntityid,
                CustId = customerRequest.CreqCustEntityid,
                AgentId = customerRequest.CreqAgenEntityid,
                CreatePolisDate = customerRequest.CreqCreateDate,
                PolisStartDate = customerRequest.CustomerInscAsset.CiasStartdate,
                PolisEndDate = customerRequest.CustomerInscAsset.CiasEnddate
            };

            await _serviceRequestSOManager.ServiceRequest.CreateServicePolisFeasibility(createServicePolisDto);

            return CreatedAtAction(nameof(GetById), new { id = customerRequest.CreqEntityid }, customerRequest);
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

        [HttpPut("request/claim")]
        public async Task<IActionResult> RequestClaimPolis([FromBody] CustomerClaimRequestDto customerClaimDto)
        {
            try
            {
                var claimedRequest = await _serviceCustomerManager.CustomerClaimService.ClaimPolis(customerClaimDto);

                //_serviceRequestSOManager.ServiceRequest.CreateClaimPolis()

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
    }
}
