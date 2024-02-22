using Contract.DTO.CR.Request;
using Contract.DTO.CR.Response;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.CR;
using Service.CR;

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
        public async Task<ActionResult<CustomerRequestDto>> GetById(int id)
        {
            var customerRequestDto = await _serviceCustomerManager.CustomerRequestService.GetByIdAsync(id, false);
            return Ok(customerRequestDto);
        }

        // POST api/<CustomerRequestController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerRequestCreateDto customerRequestDto)
        {

            var customerRequest = await _serviceCustomerManager.CustomerRequestService.CreateAsync(customerRequestDto.Adapt<CustomerRequestDto>());
            return CreatedAtAction(nameof(GetById), new { id = customerRequest.CreqEntityid }, customerRequest);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomerRequest([FromBody] CustomerRequestCreateDto customerRequestDto)
        {
            var customerRequest = await _serviceCustomerManager.CustomerRequestService.CreateCustomerRequest(customerRequestDto.Adapt<CustomerRequestDto>());
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
            await _serviceCustomerManager.CustomerRequestService.DeleteAsync(id);
            return NoContent();
        }
    }
}
