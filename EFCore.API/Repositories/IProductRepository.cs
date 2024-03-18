using EFCore.API.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace EFCore.API.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
          

      Task<Product?> GetByIdAsync(int id);


      Task<Product> CreateAsync(Product product);

      Task<Product?> UpdateAsync(int id, Product product);


       Task<Product?> DeleteAsync(int id);

    }
}
