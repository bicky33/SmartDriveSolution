using Contract.DTO.HR;
using Domain.Entities.HR;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Base;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.HR
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTypeController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public JobTypeController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<JobTypeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobType>>>GetJobType()
        {
            var categories = await _serviceManager.JobTypeService.GetAllAsync(false);

            return Ok(categories);
        }

        // GET api/<JobTypeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobType>> GetJobTypeById(string id)
        {
            var category = await _serviceManager.JobTypeService.GetJobTypeById(id, false);
            return Ok(category);
        }

        // POST api/<JobTypeController>
        [HttpPost]
        public async Task<IActionResult> CreateJobType([FromBody] JobTypeDtoCreate jobTypeDto)
        {
            var jobType = new JobTypeDto()
            {
                    JobCode = jobTypeDto.JobCode,
                    JobModifiedDate = jobTypeDto.JobModifiedDate,
                    JobDesc = jobTypeDto.JobDesc,
                    JobRateMin = jobTypeDto.JobRateMin,
                    JobRateMax = jobTypeDto.JobRateMax,

            };
            var category = await _serviceManager.JobTypeService.CreateAsync(jobType);

            return CreatedAtAction(nameof(GetJobTypeById), new { id = category.JobCode }, category);
        }

        // PUT api/<JobTypeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobType(string id, [FromBody] JobTypeDtoCreate jobTypeDto)
        {
           /* var jobType = new JobTypeDto
            {
                JobCode = jobTypeDto.JobCode,
                JobModifiedDate = jobTypeDto.JobModifiedDate,
                JobDesc = jobTypeDto.JobDesc,
                JobRateMin = jobTypeDto.JobRateMin,
                JobRateMax = jobTypeDto.JobRateMax,
            };*/
           var jobType = jobTypeDto.Adapt<JobTypeDto>();
            await _serviceManager.JobTypeService.UpdateDataAsync(id, jobType);

            return Ok(jobType);
        }

        // DELETE api/<JobTypeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _serviceManager.JobTypeService.DeleteDataAsync(id);

            return NoContent();
        }
    }
}
