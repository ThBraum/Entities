using Entidades.DTOs;

namespace Entidades.Services.Interfaces
{
    public interface IDriverMediaService
    {
        Task<List<DriverMediaDto>> GetAllAsync();
        Task<DriverMediaDto?> GetByIdAsync(int id);
        Task<DriverMediaDto> CreateAsync(CreateDriverMediaDto dto);
        Task UpdateAsync(int id, CreateDriverMediaDto dto);
        Task DeleteAsync(int id);
    }
}
