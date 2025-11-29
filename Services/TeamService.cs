using Entidades.DTOs;
using Entidades.models;
using Entidades.Repositories.Interfaces;
using Entidades.Services.Interfaces;

namespace Entidades.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repo;
        public TeamService(ITeamRepository repo) => _repo = repo;

        public async Task<List<TeamDto>> GetAllAsync()
        {
            var teams = await _repo.GetAllAsync();
            return teams.Select(t => new TeamDto
            {
                Id = t.Id,
                Name = t.Name,
                Year = t.Year,
                Drivers = t.Drivers?.Select(d => new DriverDto { Id = d.Id, Name = d.Name, RacingNumber = d.RacingNumber, TeamId = d.TeamId, TeamName = t.Name }).ToList() ?? new List<DriverDto>()
            }).ToList();
        }

        public async Task<TeamDto?> GetByIdAsync(int id)
        {
            var t = await _repo.GetByIdAsync(id);
            if (t == null) return null;
            return new TeamDto
            {
                Id = t.Id,
                Name = t.Name,
                Year = t.Year,
                Drivers = t.Drivers?.Select(d => new DriverDto { Id = d.Id, Name = d.Name, RacingNumber = d.RacingNumber, TeamId = d.TeamId, TeamName = t.Name }).ToList() ?? new List<DriverDto>()
            };
        }

        public async Task<TeamDto> CreateAsync(CreateTeamDto dto)
        {
            var team = new Team { Name = dto.Name, Year = dto.Year };
            var created = await _repo.CreateAsync(team);
            return new TeamDto { Id = created.Id, Name = created.Name, Year = created.Year };
        }

        public async Task UpdateAsync(int id, CreateTeamDto dto)
        {
            var t = await _repo.GetByIdAsync(id);
            if (t == null) throw new KeyNotFoundException("Team not found");
            t.Name = dto.Name;
            t.Year = dto.Year;
            await _repo.UpdateAsync(t);
        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
