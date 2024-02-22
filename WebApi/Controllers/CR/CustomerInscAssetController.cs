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
    public class CustomerInscAssetController : ControllerBase
    {
        private readonly IServiceCustomerManager _serviceCustomerManager;

        public CustomerInscAssetController(IServiceCustomerManager serviceCustomerManager)
        {
            _serviceCustomerManager = serviceCustomerManager;
        }

        // GET: api/<CustomerInscAssetController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerInscAssetDto>>> GetCustomerInscAssets()
        {
            var customerInscAssetDto = await _serviceCustomerManager.CustomerInscAssetService.GetAllAsync(false);
            return Ok(customerInscAssetDto);
        }

        // GET api/<CustomerInscAssetController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInscAssetDto>> GetCustomerInscAssetById(int id)
        {
            var customerInscAssetDto = await _serviceCustomerManager.CustomerInscAssetService.GetByIdAsync(id, false);
            return Ok(customerInscAssetDto);
        }

        // POST api/<CustomerInscAssetController>
        [HttpPost]
        public async Task<IActionResult> CreateCustomerInscAsset([FromBody] CustomerInscAssetCreateDto customerInscAssetDto)
        {
            var customerInscAsset = await _serviceCustomerManager.CustomerInscAssetService.CreateAsync(customerInscAssetDto.Adapt<CustomerInscAssetDto>());
            return CreatedAtAction(nameof(GetCustomerInscAssetById), new { id = customerInscAsset.CiasCreqEntityid }, customerInscAsset);
        }

        // PUT api/<CustomerRequestController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerInscAsset(int id, [FromBody] CustomerInscAssetCreateDto customerInscAssetDto)
        {
            await _serviceCustomerManager.CustomerInscAssetService.UpdateAsync(id, customerInscAssetDto.Adapt<CustomerInscAssetDto>());
            return NoContent();

        }

        // DELETE api/<CustomerInscAssetController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerInscAsset(int id)
        {
            await _serviceCustomerManager.CustomerInscAssetService.DeleteAsync(id);
            return NoContent();
        }
    }
}
