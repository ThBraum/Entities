using Entidades.models;
namespace Entidades.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetAllAsync();
        Task<Team?> GetByIdAsync(int id);
        Task<Team> CreateAsync(Team team);
        Task UpdateAsync(Team team);
        Task DeleteAsync(int id);
    }
}
