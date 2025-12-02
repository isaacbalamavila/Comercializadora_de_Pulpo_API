using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace comercializadora_de_pulpo_api.Repositories
{
    public class UnitRepository(ComercializadoraDePulpoContext context) : IUnitRepository
    {
        private readonly ComercializadoraDePulpoContext _context = context;

        public async Task<List<Unit>> GetUnitsAsync()
        {
            return await _context.Units.ToListAsync();
        }

        public async Task<Unit?> GetUnitByIdAsync(int unitId)
        {
            return await _context.Units.FirstOrDefaultAsync(u => u.Id == unitId);
        }
    }
}
