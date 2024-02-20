using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.RequestFeatured;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/master/[controller]")]
    [ApiController]
    public class ProvinsiController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public ProvinsiController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provinsi>>> Get()
        {
            var provinsi = await _serviceManagerMaster.ProvinsiService.GetAllAsync(false);
            return Ok(provinsi);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Provinsi>> Get(int id)
        {
            var provinsi = await _serviceManagerMaster.ProvinsiService.GetByIdAsync(id, false);
            return Ok(provinsi);
        }

        [HttpGet("paginate")]
        public async Task<ActionResult<IEnumerable<Provinsi>>> GetCategoriesWithPagination([FromQuery] EntityParameter entityParameter)
        {
            var provinces = await _serviceManagerMaster.ProvinsiService.GetAllWithPagingAsync(entityParameter, false);
            return Ok(provinces);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProvinsiResponse request)
        {
            if (request == null)
            {
                return BadRequest("Provinsi Request is NOT valid");
            }
            var provinsi = await _serviceManagerMaster.ProvinsiService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = provinsi.ProvId }, provinsi);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProvinsiResponse request)
        {
            await _serviceManagerMaster.ProvinsiService.UpdateAsync(id, request);
            return CreatedAtAction(nameof(Get), new { id = request.ProvId }, request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManagerMaster.ProvinsiService.DeleteAsync(id);

            return NoContent();
        }
    }
}