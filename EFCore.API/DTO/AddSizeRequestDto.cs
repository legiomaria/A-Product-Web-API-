using EFCore.API.Models;
using System.ComponentModel.DataAnnotations;


namespace EFCore.API.DTO
{
    public class AddSizeRequestDto
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }


    }
}
