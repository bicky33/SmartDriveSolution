using Contract.DTO.Partners;
using Contract.DTO.SO;
using Contract.Extensions;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Partners;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Partners
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerClaimAssetEvidenceController : ControllerBase
    {
        private readonly IServicePartnerManager _servicePartnerManager;

        public PartnerClaimAssetEvidenceController(IServicePartnerManager servicePartnerManager)
        {
            _servicePartnerManager = servicePartnerManager;
        }

        // GET: api/<PartnerClaimAssetEvidenceController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaimAssetEvidenceDto>>> Get()
        {
            IEnumerable<ClaimAssetEvidenceDto> data = await _servicePartnerManager.ServicePartnerClaimAssetEvidence.GetAllAsync(true);
            return Ok(data);
        }

        // GET api/<PartnerClaimAssetEvidenceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PartnerClaimAssetEvidenceController>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PartnerClaimAssetEvidenceRequest request)
        {
            string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            ClaimAssetEvidenceDtoCreate data = request.AsEvidenceSO();
            await _servicePartnerManager.ServicePartnerClaimAssetEvidence.CreateAsyncWithLink(data, baseUrl);
            return NoContent();
        }

        // PUT api/<PartnerClaimAssetEvidenceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PartnerClaimAssetEvidenceController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _servicePartnerManager.ServicePartnerClaimAssetEvidence.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("batch")]
        public async Task<ActionResult> PostBatch([FromForm] PartnerClaimAssetEvidenceBatchRequest request)
        {
            string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            await _servicePartnerManager.ServicePartnerClaimAssetEvidence.CreateBatch(request, baseUrl);
            return NoContent();
        }

        [HttpDelete("batch/{caspPartEntityid:int}/{caspSeroId}")]
        public async Task<ActionResult> Delete(int caspPartEntityid, string caspSeroId)
        {
            await _servicePartnerManager.ServicePartnerClaimAssetEvidence.DeleteBatch(caspPartEntityid, caspSeroId);
            return NoContent();
        }

        [HttpGet("{caspPartEntityid:int}/{caspSeroId}")]
        public async Task<ActionResult> GetByParameter(int caspPartEntityid, string caspSeroId)
        {
            IEnumerable<ClaimAssetEvidenceDto> claims = await _servicePartnerManager.ServicePartnerClaimAssetEvidence
                .GetByParameter(caspPartEntityid, caspSeroId);
            return Ok(claims);
        }

    }
}
