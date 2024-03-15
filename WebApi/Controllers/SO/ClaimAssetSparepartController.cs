using Contract.DTO.SO;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.SO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.SO
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimAssetSparepartController : ControllerBase
    {
        private readonly IServiceSOManager _serviceManager;

        public ClaimAssetSparepartController(IServiceSOManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/<ClaimAssetSparepartController>
        [HttpGet]
        public async Task<IActionResult> GetSpareparts()
        {
            var casp = await _serviceManager.ClaimAssetSparepartService.GetAllAsync(false);
            return Ok(casp);
        }

        // GET api/<ClaimAssetSparepartController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSparepartById(int id)
        {
            var caspDto = await _serviceManager.ClaimAssetSparepartService.GetByIdAsync(id, false);
            return Ok(caspDto);
        }

        // POST api/<ClaimAssetSparepartController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClaimAssetSparepartDtoCreate caspDto)
        {
            if (caspDto == null)
                return BadRequest("Service object is not valid");

            var newCaspDto = await _serviceManager.ClaimAssetSparepartService.CreateAsync(caspDto);

            return CreatedAtAction(nameof(GetSparepartById), new { id = newCaspDto.CaspSeroId }, newCaspDto);
        }

        // PUT api/<ClaimAssetSparepartController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ClaimAssetSparepartDtoCreate caspDto)
        {
            var casp = await _serviceManager.ClaimAssetSparepartService.GetByIdAsync(id, true);
            if (casp == null)
                return NotFound();
            await _serviceManager.ClaimAssetSparepartService.UpdateAsync(id, caspDto);
            return NoContent();
        }

        // DELETE api/<ClaimAssetSparepartController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var casp = await _serviceManager.ClaimAssetSparepartService.GetByIdAsync(id, false);
            if (casp == null)
                return NotFound();
            await _serviceManager.ClaimAssetSparepartService.DeleteAsync(id);
            return Ok();
        }
    }
}
