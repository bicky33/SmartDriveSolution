using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProvinsiController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public ProvinsiController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        // GET: api/<CarSeriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provinsi>>> GetAllProvinsi()
        {
            var provinsi = await _serviceManagerMaster.ProvinsiService.GetAllAsync(false);
            return Ok(provinsi);
        }

        // GET api/<CarSeriesController>/5
        [HttpGet("{id}", Name = "GetProvinsiByID")]
        public async Task<ActionResult<Provinsi>> GetProvinsiByID(int id)
        {
            var provinsi = await _serviceManagerMaster.ProvinsiService.GetByIdAsync(id, false);
            return Ok(provinsi);
        }

        // POST api/<CarSeriesController>
        [HttpPost]
        public async Task<IActionResult> CreateProvinsi([FromBody] ProvinsiResponse request)
        {
            if (request == null)
            {
                return BadRequest("Provinsi Request is NOT valid");
            }
            var provinsi = await _serviceManagerMaster.ProvinsiService.CreateAsync(request);
            return CreatedAtAction(nameof(GetProvinsiByID), new { id = provinsi.ProvId }, provinsi);
        }

        // PUT api/<CarSeriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvinsi(int id, [FromBody] ProvinsiResponse request)
        {
            await _serviceManagerMaster.ProvinsiService.UpdateAsync(id, request);
            return CreatedAtAction(nameof(GetProvinsiByID), new { id = request.ProvId }, request);
        }

        // DELETE api/<CarSeriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvinsi(int id)
        {
            await _serviceManagerMaster.ProvinsiService.DeleteAsync(id);

            return NoContent();
        }
    }
}