﻿using Contract.DTO.Master;
using Domain.Entities.Master;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Master;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Master
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManagerMaster _serviceManagerMaster;

        public CategoryController(IServiceManagerMaster serviceManagerMaster)
        {
            _serviceManagerMaster = serviceManagerMaster;
        }

        // GET: api/<CarSeriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategory()
        {
            var categories = await _serviceManagerMaster.CategoryService.GetAllAsync(false);
            return Ok(categories);
        }

        // GET api/<CarSeriesController>/5
        [HttpGet("{id}", Name = "GetCategoryByID")]
        public async Task<ActionResult<Category>> GetCategoryByID(int id)
        {
            var category = await _serviceManagerMaster.CategoryService.GetByIdAsync(id, false);
            return Ok(category);
        }

        // POST api/<CarSeriesController>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryResponse request)
        {
            if (request == null)
            {
                return BadRequest("Category Request is NOT valid");
            }
            var category = await _serviceManagerMaster.CategoryService.CreateAsync(request);
            return CreatedAtAction(nameof(GetCategoryByID), new { id = category.CateId }, category);
        }

        // PUT api/<CarSeriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryResponse request)
        {
            await _serviceManagerMaster.CategoryService.UpdateAsync(id, request);
            return CreatedAtAction(nameof(GetCategoryByID), new { id = request.CateId }, request);
        }

        // DELETE api/<CarSeriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _serviceManagerMaster.CategoryService.DeleteAsync(id);

            return NoContent();
        }
    }
}