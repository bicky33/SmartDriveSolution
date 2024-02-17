using Contract.DTO.HR;
using Domain.Entities.HR;
using Domain.Entities.Master;
using Domain.Repositories.HR.RequestFeature;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Base;
using Services.Base;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.HR
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EmployeeController(IServiceManager services)
        {
            _serviceManager = services;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
           var employees = await _serviceManager.EmployeeService.GetAllAsync(false);

            return Ok(employees);
        }

        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllPaging([FromQuery] EntityParameter entityParameter)
        {
            var categories = await _serviceManager.EmployeeService.GetAllPagingAsync(entityParameter, false);

            return Ok(categories);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var category = await _serviceManager.EmployeeService.GetByIdAsync(id, false);
            return Ok(category);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> Delete(int id)
        {
            //var category = await _serviceManager.CategoryService.GetByIdAsync(id, false);

            await _serviceManager.EmployeeService.DeleteAsync(id);

            return NoContent();
        }
    }
}
