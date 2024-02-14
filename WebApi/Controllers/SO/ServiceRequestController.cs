using Contract.DTO.SO;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.SO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.SO
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestManager _serviceRequestManager;

        public ServiceRequestController(IServiceRequestManager serviceRequestManager)
        {
            _serviceRequestManager = serviceRequestManager;
        }

        // GET: api/<ServiceController>
        [HttpPost("CreateServicePolis")]
        public async Task<IActionResult> CreateServicePolis([FromBody] CreateServicePolisDto createServicePolisDto)
        {
            await _serviceRequestManager.ServiceRequest.CreateServicePolis(createServicePolisDto);
            return Ok();
        }
        [HttpGet("debugdongplis")]
        public IActionResult Debugging()
        {
             _serviceRequestManager.ServiceRequest.Debugging();
            return Ok();
        }

        // GET: api/<ServiceRequestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ServiceRequestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ServiceRequestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ServiceRequestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ServiceRequestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
