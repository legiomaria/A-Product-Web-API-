using EFCore.API.Data;
using EFCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
           await appDbContext.Categories.AddAsync(category);
           await appDbContext.SaveChangesAsync();
            return category; 
            
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var existingCategory = await appDbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == id);
            
            if (existingCategory == null)
            {
                return null;
            }

            appDbContext.Categories.Remove(existingCategory);
            await appDbContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<List<Category>> GetAllAsync()
        {
           return await appDbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
           return await appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateAsync(int id, Category category)
        {
            var existingCategory = await appDbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = category.Name;

            await appDbContext.SaveChangesAsync();
            return existingCategory;
        }
    }
}
