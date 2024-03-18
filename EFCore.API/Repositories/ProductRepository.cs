using EFCore.API.Data;
using EFCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
          await appDbContext.Products.AddAsync(product);
            await appDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var existingProduct = await appDbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id);

            if (existingProduct == null)
            {
                return null;
            }

            appDbContext.Products.Remove(existingProduct);
            await appDbContext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<List<Product>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var products = appDbContext.Products.Include("Category").Include("Size").AsQueryable();

            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(x => x.Name.Contains(filterQuery));
                }
            }

            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = isAscending ? products.OrderBy(x => x.Name) : products.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    products = isAscending ? products.OrderBy(x => x.Price) : products.OrderByDescending(x => x.Price);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;


            return await products.Skip(skipResults).Take(pageSize).ToListAsync();

           //return await appDbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
          return await appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            var existingProduct = await appDbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            await appDbContext.SaveChangesAsync();
            return existingProduct;

        }
    }
}
