using Contract.DTO.Partners;
using Domain.RequestFeatured;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Partners;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Partners
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerAreaWorkgroupController : ControllerBase
    {
        private readonly IServicePartnerManager _servicePartnerManager;

        public PartnerAreaWorkgroupController(IServicePartnerManager servicePartnerManager)
        {
            _servicePartnerManager = servicePartnerManager;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _servicePartnerManager.ServicePartnerAreaWorkgroup.GetAllAsync(false);
            return Ok(result);
        }

        // GET: api/<ValuesController/paging>
        [HttpGet("paging")]
        public async Task<ActionResult> GetPaging([FromQuery] EntityParameter parameter)
        {
            var result = await _servicePartnerManager.ServicePartnerAreaWorkgroup.GetAllPagingAsync(parameter, false);
            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{pawoPatrEntityid:int}/{pawoUserEntityid:int}/{pawoArwgCode}")]
        public async Task<ActionResult> Get(int pawoPatrEntityid, int pawoUserEntityid, string pawoArwgCode)
        {
            var response = await _servicePartnerManager.ServicePartnerAreaWorkgroup.GetByIdAsync(pawoPatrEntityid, pawoArwgCode, pawoUserEntityid, false);
            return Ok(response);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] PartnerAreaWorkgroupDTO request)
        {
            await _servicePartnerManager.ServicePartnerAreaWorkgroup.CreateAsync(request);
            return NoContent();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{pawoPatrEntityid:int}/{pawoUserEntityid:int}/{pawoArwgCode}")]
        public async Task<ActionResult> Put(int pawoPatrEntityid, int pawoUserEntityid, string pawoArwgCode, [FromBody] PartnerAreaWorkgroupDTO request)
        {
            await _servicePartnerManager.ServicePartnerAreaWorkgroup.UpdateAsync(pawoPatrEntityid, pawoArwgCode, pawoUserEntityid, request, true);
            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{pawoPatrEntityid:int}/{pawoUserEntityid:int}/{pawoArwgCode}")]
        public async Task<ActionResult> Delete(int pawoPatrEntityid, int pawoUserEntityid, string pawoArwgCode)
        {
            await _servicePartnerManager.ServicePartnerAreaWorkgroup.DeleteAsync(PawoPatrEntityid, PawoArwgCode, PawoUserEntityid);
            return NoContent();
        }
    }
}
