using Microsoft.EntityFrameworkCore;
using Entidades.data;
using Entidades.models;
using Entidades.Repositories.Interfaces;

namespace Entidades.Repositories
{
    public class DriverMediaRepository : IDriverMediaRepository
    {
        private readonly ApiDbContext _db;
        public DriverMediaRepository(ApiDbContext db) => _db = db;

        public async Task<List<DriverMedia>> GetAllAsync() => await _db.DriverMedias.Include(dm => dm.Driver).AsNoTracking().ToListAsync();

        public async Task<DriverMedia?> GetByIdAsync(int id) => await _db.DriverMedias.Include(dm => dm.Driver).FirstOrDefaultAsync(dm => dm.Id == id);

        public async Task<DriverMedia> CreateAsync(DriverMedia media)
        {
            _db.DriverMedias.Add(media);
            await _db.SaveChangesAsync();
            return media;
        }

        public async Task UpdateAsync(DriverMedia media)
        {
            _db.Entry(media).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var m = await _db.DriverMedias.FindAsync(id);
            if (m != null)
            {
                _db.DriverMedias.Remove(m);
                await _db.SaveChangesAsync();
            }
        }
    }
}
