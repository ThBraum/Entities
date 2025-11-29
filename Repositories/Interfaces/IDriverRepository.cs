using Entidades.models;
namespace Entidades.Repositories.Interfaces
{
    public interface IDriverRepository
    {
        Task<List<Driver>> GetAllAsync();
        Task<Driver?> GetByIdAsync(int id);
        Task<Driver> CreateAsync(Driver driver);
        Task UpdateAsync(Driver driver);
        Task DeleteAsync(int id);
    }
}
