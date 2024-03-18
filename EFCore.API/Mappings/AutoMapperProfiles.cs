using AutoMapper;
using EFCore.API.DTO;
using EFCore.API.Models;

namespace EFCore.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<AddProductRequestDto, Product>().ReverseMap();

            CreateMap<UpdateProductRequestDto, Product>().ReverseMap();


            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<AddCategoryRequestDto, Category>();

            CreateMap<UpdateCategoryRequestDto, Category>();


            CreateMap<Size, SizeDto>().ReverseMap().ReverseMap();

            CreateMap<AddSizeRequestDto, Size>().ReverseMap();

            CreateMap<UpdateSizeRequestDto, Size>().ReverseMap();

            
        }
       
    }
}
