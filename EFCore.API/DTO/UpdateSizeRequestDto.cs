using System.ComponentModel.DataAnnotations;

namespace EFCore.API.DTO
{
    public class UpdateSizeRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
