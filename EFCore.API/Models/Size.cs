using System.Text.Json.Serialization;

namespace EFCore.API.Models
{
    public class Size
    {
        public int Id { get; set; }

        public string Name { get; set; }

        
        public Product Product { get; set; }

        public int ProductId { get; set; }
    }
}
