using AutoMapper;
using EFCore.API.CustomActionFilters;
using EFCore.API.Data;
using EFCore.API.DTO;
using EFCore.API.Models;
using EFCore.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        
        private readonly ISizeRepository sizeRepository;
        private readonly IMapper mapper;

        public SizesController(ISizeRepository sizeRepository,
            IMapper mapper)
        {
            this.sizeRepository = sizeRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sizesDomain = await sizeRepository.GetAllAsync();

            return Ok(mapper.Map<List<SizeDto>>(sizesDomain));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var sizeDomain = await sizeRepository.GetByIdAsync(id);

            if(sizeDomain == null)
            {
                return NotFound();
            }
       
            return Ok(mapper.Map<SizeDto>(sizeDomain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddSizeRequestDto addSizeRequestDto)
        {
                var sizeDomainModel = mapper.Map<Size>(addSizeRequestDto);

                sizeDomainModel = await sizeRepository.CreateAsync(sizeDomainModel);

                var sizeDto = mapper.Map<SizeDto>(sizeDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = sizeDto.Id }, sizeDto); 
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSizeRequestDto updateSizeRequestDto)
        {
                var sizeDomainModel = mapper.Map<Size>(updateSizeRequestDto);


                sizeDomainModel = await sizeRepository.UpdateAsync(id, sizeDomainModel);

                if (sizeDomainModel == null)
                {
                    return NotFound();
                }

            return Ok(mapper.Map<SizeDto>(sizeDomainModel));

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var sizeDomainModel = await sizeRepository.DeleteAsync(id);

            if (sizeDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<SizeDto>(sizeDomainModel));
        }
    }
}
