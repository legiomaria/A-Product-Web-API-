using System.ComponentModel.DataAnnotations;

namespace EFCore.API.DTO
{
    public class AddCategoryRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
