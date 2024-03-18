using AutoMapper;
using EFCore.API.CustomActionFilters;
using EFCore.API.Data;
using EFCore.API.DTO;
using EFCore.API.Models;
using EFCore.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            var categoriesDomain = await categoryRepository.GetAllAsync();

            return Ok(mapper.Map<List<CategoryDto>>(categoriesDomain));
        }


        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var categoryDomain = await categoryRepository.GetByIdAsync(id); 

            if (categoryDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CategoryDto>(categoryDomain)); 
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddCategoryRequestDto addCategoryRequestDto)
        {
                var categoryDomainModel = mapper.Map<Category>(addCategoryRequestDto);

                categoryDomainModel = await categoryRepository.CreateAsync(categoryDomainModel);

                var categoryDto = mapper.Map<CategoryDto>(categoryDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, categoryDto);  
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
                var categoryDomainModel = mapper.Map<Category>(updateCategoryRequestDto);

                categoryDomainModel = await categoryRepository.UpdateAsync(id, categoryDomainModel);

                if (categoryDomainModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<CategoryDto>(categoryDomainModel));  
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Writer, Reader")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var categoryDomainModel = await categoryRepository.DeleteAsync(id);

            if (categoryDomainModel == null)
            {
                return NotFound();
            }
           
            return Ok(mapper.Map<CategoryDto>(categoryDomainModel));
        }
    }
}
