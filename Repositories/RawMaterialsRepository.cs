using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace comercializadora_de_pulpo_api.Repositories
{
    public class RawMaterialsRepository(ComercializadoraDePulpoContext context)
        : IRawMaterialsRepository
    {
        private readonly ComercializadoraDePulpoContext _context = context;

        public async Task<List<RawMaterial>> GetRawMaterialsAsync()
        {
            return await _context.RawMaterials.ToListAsync();
        }

        public async Task<RawMaterial?> GetRawMaterialByIdAsync(int rawMaterialId)
        {
            return await _context.RawMaterials.FirstOrDefaultAsync(rm => rm.Id == rawMaterialId);
        }
    }
}
