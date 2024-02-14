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
    public class CustomerRequestController : ControllerBase
    {
        private readonly IServiceCustomerManager _serviceCustomerManager;

        public CustomerRequestController(IServiceCustomerManager serviceCustomerManager)
        {
            _serviceCustomerManager = serviceCustomerManager;
        }

        // GET: api/<CustomerRequestController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerRequestDto>>> GetCustomerRequests()
        {
            var customerRequestDto = await _serviceCustomerManager.CustomerRequestService.GetAllAsync(false);
            return Ok(customerRequestDto);
        }

        // GET api/<CustomerRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerRequestDto>> GetCustomerRequestById(int id)
        {
            var customerRequestDto = await _serviceCustomerManager.CustomerRequestService.GetByIdAsync(id, false);
            return Ok(customerRequestDto);
        }

        // POST api/<CustomerRequestController>
        [HttpPost]
        public async Task<IActionResult> CreateCustomerRequest([FromBody] CustomerRequestCreateDto customerRequestDto)
        {

            var customerRequest = await _serviceCustomerManager.CustomerRequestService.CreateAsync(customerRequestDto.Adapt<CustomerRequestDto>());
            return CreatedAtAction(nameof(GetCustomerRequestById), new { id = customerRequest.CreqEntityid }, customerRequest);
        }

        // PUT api/<CustomerRequestController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerRequest(int id, [FromBody] CustomerRequestUpdateDto customerRequestDto)
        {
            await _serviceCustomerManager.CustomerRequestService.UpdateAsync(id, customerRequestDto.Adapt<CustomerRequestDto>());
            return NoContent();

        }

        // DELETE api/<CustomerRequestController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerRequest(int id)
        {
            await _serviceCustomerManager.CustomerRequestService.DeleteAsync(id);
            return NoContent();
        }
    }
}
