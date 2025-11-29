using Entidades.DTOs;
using Entidades.models;
using Entidades.Repositories.Interfaces;
using Entidades.Services.Interfaces;

namespace Entidades.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _repo;
        private readonly ITeamRepository _teamRepo;
        public DriverService(IDriverRepository repo, ITeamRepository teamRepo)
        {
            _repo = repo;
            _teamRepo = teamRepo;
        }

        public async Task<List<DriverDto>> GetAllAsync()
        {
            var drivers = await _repo.GetAllAsync();
            return drivers.Select(d => new DriverDto { Id = d.Id, Name = d.Name, RacingNumber = d.RacingNumber, TeamId = d.TeamId, TeamName = d.Team?.Name }).ToList();
        }

        public async Task<DriverDto?> GetByIdAsync(int id)
        {
            var d = await _repo.GetByIdAsync(id);
            if (d == null) return null;
            return new DriverDto { Id = d.Id, Name = d.Name, RacingNumber = d.RacingNumber, TeamId = d.TeamId, TeamName = d.Team?.Name };
        }

        public async Task<DriverDto> CreateAsync(CreateDriverDto dto)
        {
            var team = await _teamRepo.GetByIdAsync(dto.TeamId);
            if (team == null) throw new KeyNotFoundException("Team not found");
            var driver = new Driver { Name = dto.Name, RacingNumber = dto.RacingNumber, TeamId = dto.TeamId };
            var created = await _repo.CreateAsync(driver);
            return new DriverDto { Id = created.Id, Name = created.Name, RacingNumber = created.RacingNumber, TeamId = created.TeamId, TeamName = team.Name };
        }

        public async Task UpdateAsync(int id, CreateDriverDto dto)
        {
            var d = await _repo.GetByIdAsync(id);
            if (d == null) throw new KeyNotFoundException("Driver not found");
            d.Name = dto.Name;
            d.RacingNumber = dto.RacingNumber;
            d.TeamId = dto.TeamId;
            await _repo.UpdateAsync(d);
        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
