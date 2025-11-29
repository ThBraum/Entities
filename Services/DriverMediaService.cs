using Entidades.DTOs;
using Entidades.models;
using Entidades.Repositories.Interfaces;
using Entidades.Services.Interfaces;

namespace Entidades.Services
{
    public class DriverMediaService : IDriverMediaService
    {
        private readonly IDriverMediaRepository _repo;
        private readonly IDriverRepository _driverRepo;
        public DriverMediaService(IDriverMediaRepository repo, IDriverRepository driverRepo)
        {
            _repo = repo;
            _driverRepo = driverRepo;
        }

        public async Task<List<DriverMediaDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(m => new DriverMediaDto { Id = m.Id, DriverId = m.DriverId, Media = m.Media, DriverName = m.Driver?.Name }).ToList();
        }

        public async Task<DriverMediaDto?> GetByIdAsync(int id)
        {
            var m = await _repo.GetByIdAsync(id);
            if (m == null) return null;
            return new DriverMediaDto { Id = m.Id, DriverId = m.DriverId, Media = m.Media, DriverName = m.Driver?.Name };
        }

        public async Task<DriverMediaDto> CreateAsync(CreateDriverMediaDto dto)
        {
            var driver = await _driverRepo.GetByIdAsync(dto.DriverId);
            if (driver == null) throw new KeyNotFoundException("Driver not found");
            var media = new DriverMedia { DriverId = dto.DriverId, Media = dto.Media };
            var created = await _repo.CreateAsync(media);
            return new DriverMediaDto { Id = created.Id, DriverId = created.DriverId, Media = created.Media, DriverName = driver.Name };
        }

        public async Task UpdateAsync(int id, CreateDriverMediaDto dto)
        {
            var m = await _repo.GetByIdAsync(id);
            if (m == null) throw new KeyNotFoundException("DriverMedia not found");
            m.DriverId = dto.DriverId;
            m.Media = dto.Media;
            await _repo.UpdateAsync(m);
        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
