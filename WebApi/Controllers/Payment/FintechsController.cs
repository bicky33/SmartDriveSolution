using Contract.DTO.Payment;
using Domain.Entities.Master;
using Domain.Entities.Payment;
using Domain.Repositories.Base;
using Domain.Repositories.Payment;
using Mapster;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class FintechsController : ControllerBase
    {
        private readonly IRepositoryPaymentManager _repositoryManager;

        public FintechsController(IRepositoryPaymentManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }


        // GET: api/<FintechsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fintech>>> GetFintechs()
        {
            var fintechs = await _repositoryManager.FintechRepository.GetAllEntity(false);
            var fintechDto = fintechs.Adapt<IEnumerable<FintechDto>>();
            return Ok(fintechDto);
        }

        // GET api/<FintechsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FintechDto>> GetFintechById(int id)
        {
            var fintech = _repositoryManager.FintechRepository.GetEntityById(id, false);
            if (fintech == null)
                return NotFound("ID your are looking for is not exist");
            var fintechDto = fintech.Adapt<FintechDto>();
            return Ok(fintechDto);
        }

        // POST api/<FintechsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FintechDto fintechDto)
        {
            if (fintechDto == null)
            {
                return BadRequest("Bank object is not valid");
            }
            var fintech = fintechDto.Adapt<Fintech>();
            _repositoryManager.FintechRepository.CreateEntity(fintech);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

            return Ok(fintechDto);
        }

        // PUT api/<FintechsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FintechDto fintechDto)
        {
            var fintech = await _repositoryManager.FintechRepository.GetEntityById(id, false);
            if (fintech == null)
                return NotFound("ID your are looking for is not exist");

            fintech.FintName = fintechDto.FintName;
            fintech.FintDesc = fintechDto.FintDesc;

            return Ok(fintech);

        }

        // DELETE api/<FintechsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fintech = _repositoryManager.FintechRepository.GetEntityById(id, false);
            if (fintech == null)
                return NotFound("ID your are looking for is not exist");

            var fintechDto = fintech.Adapt<Fintech>();
            _repositoryManager.FintechRepository.DeleteEntity(fintechDto);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

            return Ok($"ID {id} Succesfully deleted");

        }
    }
}
