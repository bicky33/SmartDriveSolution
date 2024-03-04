using Contract.DTO.Partners;
using Contract.DTO.Payment;
using Domain.Enum;
using Domain.Exceptions;
using Domain.RequestFeatured;
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


        // POST api/<PaymentTransactionsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] PaymentTransactionCreateDto paymentDto)
        {
            var from = await _serviceManager.UserAccountService.GetByAccountNoAsync(paymentDto.PatrUsacAccountNoFrom, false, ReturnException.RETURN_WHEN_NULL);

            if (from.UsacDebet <= paymentDto.SendAmount)
                return BadRequest("Balance is not enough");

            //await _serviceManager.UserAccountService.GetByAccountNoAsync(paymentDto.PatrUsacAccountNoTo, false, ReturnException.RETURN_WHEN_NULL);

            if (paymentDto == null)
                return BadRequest("Payment object is not valid");

            var a = await _serviceManager.PaymentTransactionService.CreateAsync(paymentDto);
            return Ok(a);
        }

        // POST api/<PaymentTransactionsController>
        [HttpPost]
        [Route("AddDeposit")]
        public async Task<IActionResult> Deposit([FromBody] PaymentTransactionDepositDto paymentDto)
        {
            //if (paymentDto.SendAmount)
            //    return BadRequest("Balance is not enough");

            await _serviceManager.UserAccountService.GetByAccountNoAsync(paymentDto.PatrUsacAccountNoTo, false, ReturnException.RETURN_WHEN_NULL);

            if (paymentDto == null)
                return BadRequest("Payment object is not valid");

            var a = await _serviceManager.PaymentTransactionService.CreateDepositAsync(paymentDto);
            return Ok(a);
        }

        //TODO Paging with parameter userid
        // GET: api/<PartnerController/paging>
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PartnerDTO>>> GetPaging([FromQuery] EntityPaymentTransactionParameter request)
        {
            var response = await _serviceManager.PaymentTransactionService.GetAllPagingAsync(request, false);
            return Ok(response);
        }

        [HttpGet("pagingCount")]
        public async Task<ActionResult<IEnumerable<PartnerDTO>>> GetPagingCount([FromQuery] EntityPaymentTransactionParameter request)
        {
            var response = await _serviceManager.PaymentTransactionService.GetAllPagingAsync(request, false);
            int count = response.Count();
            return Ok(count);
        }

    }
}
