using System.ComponentModel.DataAnnotations;

namespace EFCore.API.DTO
{
    public class UpdateProductRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }

        public double Price { get; set; }
    }
}
