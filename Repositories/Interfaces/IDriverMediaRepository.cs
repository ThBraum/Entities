using Entidades.models;

namespace Entidades.Repositories.Interfaces
{
    public interface IDriverMediaRepository
    {
        Task<List<DriverMedia>> GetAllAsync();
        Task<DriverMedia?> GetByIdAsync(int id);
        Task<DriverMedia> CreateAsync(DriverMedia media);
        Task UpdateAsync(DriverMedia media);
        Task DeleteAsync(int id);
    }
}
