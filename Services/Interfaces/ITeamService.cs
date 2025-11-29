using Entidades.DTOs;

namespace Entidades.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamDto>> GetAllAsync();
        Task<TeamDto?> GetByIdAsync(int id);
        Task<TeamDto> CreateAsync(CreateTeamDto dto);
        Task UpdateAsync(int id, CreateTeamDto dto);
        Task DeleteAsync(int id);
    }
}
