using Microsoft.EntityFrameworkCore;
using Entidades.data;
using Entidades.models;
using Entidades.Repositories.Interfaces;

namespace Entidades.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly ApiDbContext _db;
        public DriverRepository(ApiDbContext db) => _db = db;

        public async Task<List<Driver>> GetAllAsync() => await _db.Drivers.Include(d => d.Team).AsNoTracking().ToListAsync();

        public async Task<Driver?> GetByIdAsync(int id) => await _db.Drivers.Include(d => d.Team).FirstOrDefaultAsync(d => d.Id == id);

        public async Task<Driver> CreateAsync(Driver driver)
        {
            _db.Drivers.Add(driver);
            await _db.SaveChangesAsync();
            return driver;
        }

        public async Task UpdateAsync(Driver driver)
        {
            _db.Entry(driver).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var driver = await _db.Drivers.FindAsync(id);
            if (driver != null)
            {
                _db.Drivers.Remove(driver);
                await _db.SaveChangesAsync();
            }
        }
    }
}
