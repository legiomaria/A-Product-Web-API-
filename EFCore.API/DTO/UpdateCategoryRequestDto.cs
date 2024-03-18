using System.ComponentModel.DataAnnotations;

namespace EFCore.API.DTO
{
    public class UpdateCategoryRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
    }
}
