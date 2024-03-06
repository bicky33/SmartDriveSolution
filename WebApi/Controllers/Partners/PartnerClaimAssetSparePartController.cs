using Contract.DTO.Partners;
using Contract.DTO.SO;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Partners;
using Service.Partners;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Partners
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerClaimAssetSparePartController : ControllerBase
    {
        private readonly IServicePartnerManager _servicePartnerManager;

        public PartnerClaimAssetSparePartController(IServicePartnerManager servicePartnerManager)
        {
            _servicePartnerManager = servicePartnerManager;
        }

        // GET: api/<PartnerClaimAssetSparePartController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaimAssetSparepartDto>>> Get()
        {
           IEnumerable<ClaimAssetSparepartDto> response =  await _servicePartnerManager.ServicePartnerClaimAssetSparepart.GetAllAsync(false);
           return Ok(response);
        }

        // GET api/<PartnerClaimAssetSparePartController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClaimAssetSparepartDto>> Get(int id)
        {
            ClaimAssetSparepartDto response = await _servicePartnerManager.ServicePartnerClaimAssetSparepart.GetByIdAsync(id, false);
            return Ok(response);
        }

        // POST api/<PartnerClaimAssetSparePartController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClaimAssetSparepartDtoCreate request)
        {
           await _servicePartnerManager.ServicePartnerClaimAssetSparepart.CreateAsync(request);
           return NoContent();
        }

        // PUT api/<PartnerClaimAssetSparePartController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClaimAssetSparepartDtoCreate caspDto)
        {
            await _servicePartnerManager.ServicePartnerClaimAssetSparepart.UpdateAsync(id, caspDto);
            return NoContent();
        }

        // DELETE api/<PartnerClaimAssetSparePartController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _servicePartnerManager.ServicePartnerClaimAssetSparepart.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{caspPartEntityid:int}/{caspSeroId}")]
        public async Task<ActionResult<IEnumerable<ClaimAssetSparepartDto>>> GetByParams(int caspPartEntityid, string caspSeroId)
        {
            IEnumerable<ClaimAssetSparepartDto> claims = await _servicePartnerManager.ServicePartnerClaimAssetSparepartBatch.GetByParameter(caspPartEntityid, caspSeroId);
            return Ok(claims);
        }

        [HttpPost("batch")]
        public async Task<ActionResult> PostBatch([FromBody] List<ClaimAssetSparepartDtoCreate> request)
        {
            await _servicePartnerManager.ServicePartnerClaimAssetSparepartBatch.CreateBatch(request);
            return NoContent();
        }

        [HttpDelete("batch/{caspPartEntityid:int}/{caspSeroId}")]
        public async Task<ActionResult> Delete(int caspPartEntityid, string caspSeroId)
        {
            await _servicePartnerManager.ServicePartnerClaimAssetSparepartBatch.DeleteBatch(caspPartEntityid, caspSeroId);
            return NoContent();
        }
    }
}
