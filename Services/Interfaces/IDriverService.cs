using Entidades.DTOs;

namespace Entidades.Services.Interfaces
{
    public interface IDriverService
    {
        Task<List<DriverDto>> GetAllAsync();
        Task<DriverDto?> GetByIdAsync(int id);
        Task<DriverDto> CreateAsync(CreateDriverDto dto);
        Task UpdateAsync(int id, CreateDriverDto dto);
        Task DeleteAsync(int id);
    }
}
