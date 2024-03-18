using EFCore.API.Models;

namespace EFCore.API.Repositories
{
    public interface ISizeRepository
    {
        Task<List<Size>> GetAllAsync();

        Task<Size?> GetByIdAsync(int id);

        Task<Size> CreateAsync(Size size);

        Task<Size?> UpdateAsync(int id, Size size);

        Task<Size?> DeleteAsync(int id);
    }
}
