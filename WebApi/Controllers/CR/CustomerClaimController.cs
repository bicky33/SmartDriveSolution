using Contract.DTO.CR.Request;
using Contract.DTO.CR.Response;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.CR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.CR
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerClaimController : ControllerBase
    {
        private readonly IServiceCustomerManager _serviceCustomerManager;

        public CustomerClaimController(IServiceCustomerManager serviceCustomerManager)
        {
            _serviceCustomerManager = serviceCustomerManager;
        }

        // GET: api/<CustomerClaimController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerClaimDto>>> GetCustomerClaims()
        {
            var customerClaimDto = await _serviceCustomerManager.CustomerClaimService.GetAllAsync(false);
            return Ok(customerClaimDto);
        }

        // GET api/<CustomerClaimController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerClaimDto>> GetCustomerClaimById(int id)
        {
            var customerClaimDto = await _serviceCustomerManager.CustomerClaimService.GetByIdAsync(id, false);
            return Ok(customerClaimDto);
        }

        // POST api/<CustomerClaimController>
        [HttpPost]
        public async Task<IActionResult> CreateCustomerClaim([FromBody] CustomerClaimCreateDto customerClaimDto)
        {
            var customerClaim = await _serviceCustomerManager.CustomerClaimService.CreateAsync(customerClaimDto.Adapt<CustomerClaimDto>());
            return CreatedAtAction(nameof(GetCustomerClaimById), new { id = customerClaim.CuclCreqEntityid }, customerClaim);
        }

        // PUT api/<CustomerClaimController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerClaim(int id, [FromBody] CustomerClaimUpdateDto customerClaimDto)
        {
            await _serviceCustomerManager.CustomerClaimService.UpdateAsync(id, customerClaimDto.Adapt<CustomerClaimDto>());
            return NoContent();
        }

        // DELETE api/<CustomerClaimController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerClaim(int id)
        {
            await _serviceCustomerManager.CustomerClaimService.DeleteAsync(id);
            return NoContent();
        }
    }
}
