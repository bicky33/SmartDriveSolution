using Contract.DTO.HR;
using Contract.DTO.HR.CompositeDto;
using Contract.DTO.HR.UpdateEmployee;
using Domain.Entities.HR;
using Domain.Entities.Master;
using Domain.RequestFeatured;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.HR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.HR
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IServiceHRManager _serviceManager;

        public EmployeeController(IServiceHRManager services)
        {
            _serviceManager = services;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
           var employees = await _serviceManager.EmployeeService.GetData(false);
            
            return Ok(employees);
        }

        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllPaging([FromQuery] EntityParameter entityParameter)
        {
            var categories = await _serviceManager.EmployeeService.GetAllPagingAsync(entityParameter, false);

            return Ok(categories);
        }

        // GET api/<EmployeeController>/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var category = await _serviceManager.EmployeeService.GetByIdAsync(id, false);
            return Ok(category);
        }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> FindEmployeeById(int id)
        {
            var category = await _serviceManager.EmployeeService.FindEmployeeById(id);
            return Ok(category);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public  async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDto employeeDto)
        {
            // employeeDto.UserComposite.UserEntityid = employeeDto.Entityid;
            
            var emp = await _serviceManager.EmployeeService.CreateEmployee(employeeDto);


          return Ok(emp);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateData(int id, [FromBody] EmployeeUpdateDto value)
        {
            await _serviceManager.EmployeeService.UpdateData(id, value);

            return Ok(value);
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
