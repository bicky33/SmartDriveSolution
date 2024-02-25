using Contract.DTO.Partners;
using Domain.RequestFeatured;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Partners;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Partners
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly IServicePartnerManager _servicePartnerManager;

        public PartnerController(IServicePartnerManager servicePartnerManager)
        {
            _servicePartnerManager = servicePartnerManager;
        }

        // GET: api/<PartnerController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartnerDTO>>> Get()
        {
            var response = await _servicePartnerManager.ServicePartner.GetAllAsync(false);
            return Ok(response);
        }

        // GET: api/<PartnerController/paging>
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PartnerDTO>>> GetPaging([FromQuery] EntityParameter request)
        {
            var response = await _servicePartnerManager.ServicePartner.GetAllPagingAsync(request, false);
            return Ok(response);
        }
        // GET api/<PartnerController>/5
        [HttpGet("{id}")]
        public async Task<PartnerDTO> Get(int id)
        {
            return await _servicePartnerManager.ServicePartner.GetByIdAsync(id, false);
        }

        // POST api/<PartnerController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PartnerDTO request)
        {
            var response = await _servicePartnerManager.ServicePartner.CreateAsync(request);
            return Ok(response);
        }

        // PUT api/<PartnerController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PartnerDTO request)
        {
            await _servicePartnerManager.ServicePartner.UpdateAsync(id, request);
            return NoContent();
        }

        // DELETE api/<PartnerController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _servicePartnerManager.ServicePartner.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("workorder")]
        public async Task<ActionResult> GetWorkorder([FromQuery] int seroPartId, [FromQuery] string seotArwgCode)
        {
            IEnumerable<PartnerWorkOrderResponse> data = await _servicePartnerManager.ServicePartnerWorkOrder.GetAll(seroPartId, seotArwgCode);
            return Ok(data);

        }
    }
}
