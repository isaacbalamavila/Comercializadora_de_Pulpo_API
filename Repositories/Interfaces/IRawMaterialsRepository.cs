using comercializadora_de_pulpo_api.Models;

namespace comercializadora_de_pulpo_api.Repositories.Interfaces
{
    public interface IRawMaterialsRepository
    {
        Task<List<RawMaterial>> GetRawMaterialsAsync();
        Task<RawMaterial?> GetRawMaterialByIdAsync(int rawMaterialId);
    }
}
