using Microsoft.EntityFrameworkCore;
using Entidades.data;
using Entidades.models;
using Entidades.Repositories.Interfaces;

namespace Entidades.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApiDbContext _db;
        public TeamRepository(ApiDbContext db) => _db = db;

        public async Task<List<Team>> GetAllAsync() => await _db.Teams.AsNoTracking().ToListAsync();

        public async Task<Team?> GetByIdAsync(int id) => await _db.Teams.Include(t => t.Drivers).FirstOrDefaultAsync(t => t.Id == id);

        public async Task<Team> CreateAsync(Team team)
        {
            _db.Teams.Add(team);
            await _db.SaveChangesAsync();
            return team;
        }

        public async Task UpdateAsync(Team team)
        {
            _db.Entry(team).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var team = await _db.Teams.FindAsync(id);
            if (team != null)
            {
                _db.Teams.Remove(team);
                await _db.SaveChangesAsync();
            }
        }
    }
}
