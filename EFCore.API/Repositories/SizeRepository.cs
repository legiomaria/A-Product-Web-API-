using EFCore.API.Data;
using EFCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Repositories
{
    public class SizeRepository : ISizeRepository
    {
        private readonly AppDbContext appDbContext;

        public SizeRepository(AppDbContext appDbContext)
        {
           this.appDbContext = appDbContext;
        }

        public async Task<Size> CreateAsync(Size size)
        {
           await appDbContext.Sizes.AddAsync(size);
            await appDbContext.SaveChangesAsync();
            return size;
        }

        public async Task<Size?> DeleteAsync(int id)
        {
            var existingSize = await appDbContext.Sizes.FirstOrDefaultAsync(x => x.Id == id);

            if(existingSize == null)
            {
                return null;
            }

             appDbContext.Remove(existingSize);
            await appDbContext.SaveChangesAsync();
            return existingSize;
        }

        public async Task<List<Size>> GetAllAsync()
        {
           return await appDbContext.Sizes.ToListAsync();
        }

        public async Task<Size?> GetByIdAsync(int id)
        {
           return await appDbContext.Sizes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Size?> UpdateAsync(int id, Size size)
        {
            var existingSize = await appDbContext.Sizes.FirstOrDefaultAsync(x => x.Id == id);
            
            if(existingSize == null)
            {
                return null;
            }

            existingSize.Name = size.Name;

            await appDbContext.SaveChangesAsync();
            return existingSize;
        }
    }
}
