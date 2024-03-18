using Contract.DTO.SO;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.SO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.SO
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimAssetEvidenceController : ControllerBase
    {
        private readonly IServiceSOManager _serviceManager;

        public ClaimAssetEvidenceController(IServiceSOManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/<ClaimAssetEvidenceController>
        [HttpGet]
        public async Task<IActionResult> GetEvidences()
        {
            var caevs = await _serviceManager.ClaimAssetEvidenceService.GetAllAsync(false);
            return Ok(caevs);
        }

        // GET api/<ClaimAssetEvidenceController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetThat(int id)
        {
            var caevDto = await _serviceManager.ClaimAssetEvidenceService.GetByIdAsync(id, false);
            return Ok(caevDto);
        }

        // POST api/<ClaimAssetEvidenceController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ClaimAssetEvidenceDtoCreate caevDto)
        {
            if (caevDto == null)
                return BadRequest("Service object is not valid");

            var newCaevDto = await _serviceManager.ClaimAssetEvidenceService.CreateAsync(caevDto);

            return Ok(newCaevDto);
            //return CreatedAtAction(nameof(GetThat), new {  id = newCaevDto.CaevId }, newCaevDto);
        }

        // PUT api/<ClaimAssetEvidenceController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ClaimAssetEvidenceDtoCreate caevDto)
        {
            var caev = await _serviceManager.ClaimAssetEvidenceService.GetByIdAsync(id, true);
            if (caev == null)
                return NotFound();
            await _serviceManager.ClaimAssetEvidenceService.UpdateAsync(id, caevDto);
            return NoContent();
        }

        // DELETE api/<ClaimAssetEvidenceController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var caev = await _serviceManager.ClaimAssetEvidenceService.GetByIdAsync(id, false);
            if (caev == null)
                return NotFound();
            await _serviceManager.ClaimAssetEvidenceService.DeleteAsync(id);
            return Ok();
        }
    }
}
