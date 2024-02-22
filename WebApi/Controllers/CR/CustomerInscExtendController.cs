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
    public class CustomerInscExtendController : ControllerBase
    {
        private readonly IServiceCustomerManager _serviceCustomerManager;

        public CustomerInscExtendController(IServiceCustomerManager serviceCustomerManager)
        {
            _serviceCustomerManager = serviceCustomerManager;
        }

        // GET: api/<CustomerInscExtendController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerInscExtendDto>>> GetCustomerInscExtends()
        {
            var customerInscExtendDto = await _serviceCustomerManager.CustomerInscExtendService.GetAllAsync(false);
            return Ok(customerInscExtendDto);
        }

        // GET api/<CustomerInscExtendController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInscExtendDto>> GetCustomerInscExtendById(int id)
        {
            var customerInscExtendDto = await _serviceCustomerManager.CustomerInscExtendService.GetByIdAsync(id, false);
            return Ok(customerInscExtendDto);
        }

        // POST api/<CustomerInscExtendController>
        [HttpPost]
        public async Task<IActionResult> CreateCustomerInscExtend([FromBody] CustomerInscExtendRequestDto customerInscExtendDto)
        {
            var customerInscExtend = await _serviceCustomerManager.CustomerInscExtendService.CreateAsync(customerInscExtendDto.Adapt<CustomerInscExtendDto>());
            return CreatedAtAction(nameof(GetCustomerInscExtendById), new { id = customerInscExtend.CuexId }, customerInscExtend);
        }

        // PUT api/<CustomerInscExtendController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerInscExtend(int id, [FromBody] CustomerInscExtendRequestDto customerInscExtendDto)
        {
            await _serviceCustomerManager.CustomerInscExtendService.UpdateAsync(id, customerInscExtendDto.Adapt<CustomerInscExtendDto>());
            return NoContent();
        }

        // DELETE api/<CustomerInscExtendController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerInscExtend(int id)
        {
            await _serviceCustomerManager.CustomerInscExtendService.DeleteAsync(id);
            return NoContent();
        }
    }
}
