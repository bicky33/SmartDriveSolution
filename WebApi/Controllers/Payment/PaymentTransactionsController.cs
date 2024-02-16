using Contract.DTO.Payment;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Payment;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTransactionsController : ControllerBase
    {
        private readonly IServicePaymentManager _serviceManager;

        public PaymentTransactionsController(IServicePaymentManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/<PaymentTransactionsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentTransactionDto>>> GetAllPayment()
        {
            var dto = await _serviceManager.PaymentTransactionService.GetAllAsync(false);
            return Ok(dto);
        }

        // GET api/<PaymentTransactionsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<PaymentTransactionsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentTransactionCreateDto paymentDto)
        {
            if (paymentDto == null)
                return BadRequest("Bank object is not valid");

            var a = await _serviceManager.PaymentTransactionService.CreateAsync(paymentDto);
            return Ok(a);
        }

        // PUT api/<PaymentTransactionsController>/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, [FromBody] PaymentTransactionDto paymentDto)
        //{
        //    return Ok("Created");

        // DELETE api/<PaymentTransactionsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

    }
}
