using Contract.DTO.Partners;
using Domain.RequestFeatured;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Partners;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Partners
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerBatchInvoiceController : ControllerBase
    {
        private readonly IServicePartnerManager _servicePartnerManager;

        public PartnerBatchInvoiceController(IServicePartnerManager servicePartnerManager)
        {
            _servicePartnerManager = servicePartnerManager;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PartnerBatchInvoiceResponse>>> GetPaging([FromQuery] EntityParameter parameter)
        {
            IEnumerable<PartnerBatchInvoiceResponse> invoices = await _servicePartnerManager.ServicePartnerBatchInvoice.GetAllPagingAsync(parameter);
            return Ok(invoices);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> Post()
        {
            await _servicePartnerManager.ServicePartnerBatchInvoice.CreateBatch();
            return NoContent();
        }
    }
}
