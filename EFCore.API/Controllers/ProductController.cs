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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductController( IProductRepository productRepository,
            IMapper mapper)
        {
           
            this.productRepository = productRepository;
            this.mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            // Get Data From Database to Domain models
            var productsModel = await productRepository.GetAllAsync(filterOn, filterQuery, sortBy, 
                isAscending ?? true, pageNumber, pageSize);

            // Return Dtos
            return Ok(mapper.Map<List<ProductDto>>(productsModel));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            // Get Product Domain Model from the Database
            var productModels = await productRepository.GetByIdAsync(id);

            if(productModels == null)
            {
                return NotFound();
            }
            // Return Dto back to the client
            return Ok(mapper.Map<ProductDto>(productModels));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddProductRequestDto addProductRequestDto)
        {
               //Map or Convert Dto to Domain Model
                var productDomainModel = mapper.Map<Product>(addProductRequestDto);

                //Use Domain Model to create Product
                productDomainModel = await productRepository.CreateAsync(productDomainModel);

                // Map Domain model back to Dto
                var productDto = mapper.Map<ProductDto>(productDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);       
     
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]UpdateProductRequestDto updateProductRequestDto)
        {
                // Map Dto to Domain Model
                var productDomainModel = mapper.Map<Product>(updateProductRequestDto);

                // Check if product exist
                productDomainModel = await productRepository.UpdateAsync(id, productDomainModel);

                if (productDomainModel == null)
                {
                    return NotFound();
                }

            return Ok(mapper.Map<ProductDto>(productDomainModel));     
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var productDomainModel = await productRepository.DeleteAsync(id);

            if (productDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProductDto>(productDomainModel));
        }
    } 
}
