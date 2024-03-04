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
    public class CustomerInscDocController : ControllerBase
    {
        private readonly IServiceCustomerManager _serviceCustomerManager;

        public CustomerInscDocController(IServiceCustomerManager serviceCustomerManager)
        {
            _serviceCustomerManager = serviceCustomerManager;
        }

        // GET: api/<CustomerInscDocController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerInscDocDto>>> GetCustomerInscDocs()
        {
            var customerInscDocDto = await _serviceCustomerManager.CustomerInscDocService.GetAllAsync(false);
            return Ok(customerInscDocDto);
        }

        // GET api/<CustomerInscDocController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInscDocDto>> GetCustomerInscDocById(int id)
        {
            var customerInscDocDto = await _serviceCustomerManager.CustomerInscDocService.GetByIdAsync(id, false);
            return Ok(customerInscDocDto);
        }

        // POST api/<CustomerInscDocController>
        [HttpPost]
        public async Task<IActionResult> CreateCustomerInscDoc([FromBody] CustomerInscDocCreateDto customerInscDocDto)
        {
            var customerInscDoc = await _serviceCustomerManager.CustomerInscDocService.CreateAsync(customerInscDocDto.Adapt<CustomerInscDocDto>());
            return CreatedAtAction(nameof(GetCustomerInscDocById), new { id = customerInscDoc.CadocId }, customerInscDoc);
        }   

        // PUT api/<CustomerInscDocController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerInscDoc(int id, [FromBody] CustomerInscDocUpdateDto customerInscDocDto)
        {
            await _serviceCustomerManager.CustomerInscDocService.UpdateAsync(id, customerInscDocDto.Adapt<CustomerInscDocDto>());
            return NoContent();
        }

        // DELETE api/<CustomerInscDocController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerInscDoc(int id)
        {
            await _serviceCustomerManager.CustomerInscDocService.DeleteAsync(id);
            return NoContent();
        }
    }
}
