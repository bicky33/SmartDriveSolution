using Contract.DTO.HR;
using Domain.Entities.HR;
using Domain.RequestFeatured;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.HR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.HR
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeArwgController : ControllerBase
    {
        private readonly IServiceHRManager _serviceManager;

        public EmployeeArwgController(IServiceHRManager serviceHRManager)
        {
            _serviceManager = serviceHRManager;
        }

        // GET: api/<EmployeeArwgController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeAreWorkgroup>>> GetEmployeeArwg()
        {
            var categories = await _serviceManager.EmployeeArwgService.GetAllData(false);

            return Ok(categories);
        }

        // GET api/<EmployeeArwgController>/5
        /*        [HttpGet("{id}")]
                public async Task<ActionResult<EmployeeAreWorkgroup>> GetEmployeArwgById(int id)
                {
                    var arwg = await _serviceManager.EmployeeArwgService.GetByIdAsync(id, false);
                    return Ok(arwg);
                }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeAreWorkgroup>> GetEmployeArwgById(int id)
        {
            var arwg = await _serviceManager.EmployeeArwgService.FindEmployeeById(id);
            return Ok(arwg);
        }

        // POST api/<EmployeeArwgController>
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeArwg([FromBody] EmployeeArwgCreateDto arwgDto)
        {
           
           /* var arwg = new EmployeeAreaWorkGroupDto()
            {
                EawgId = arwgDto.EawgId,
                EawgEntityid = arwgDto.EawgEntityid,    
                EawgStatus = arwgDto.EawgStatus,
                EawgArwgCode = arwgDto.EawgArwgCode,
                EawgModifiedDate = arwgDto.EawgModifiedDate,    
            };*/
        var data = arwgDto.Adapt<EmployeeAreaWorkGroupDto>();
        await _serviceManager.EmployeeArwgService.CreateAsync(data);

            return CreatedAtAction(nameof(GetEmployeArwgById), new { id = data.EawgId }, data);
        }

        // PUT api/<EmployeeArwgController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeArwg(int id, [FromBody] EmployeeArwgCreateDto value)
        {
            var arwg = value.Adapt<EmployeeAreaWorkGroupDto>();
            await _serviceManager.EmployeeArwgService.UpdateAsync(id, arwg);

            return Ok(arwg);
        }

        // DELETE api/<EmployeeArwgController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManager.EmployeeArwgService.DeleteAsync(id);

            return NoContent();
        }


        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<EmployeeAreWorkgroup>>> GetAllPaging([FromQuery] EntityParameter entityParameter)
        {
            var arwg = await _serviceManager.EmployeeArwgService.GetAllPagingAsync(entityParameter, false);
           // var arwgDto = arwg.Adapt<EmployeeAreaWorkGroupShowDto>();
            return Ok(arwg);
        }
    }
}
