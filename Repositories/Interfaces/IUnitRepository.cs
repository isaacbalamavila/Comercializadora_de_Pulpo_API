using comercializadora_de_pulpo_api.Models;

namespace comercializadora_de_pulpo_api.Repositories.Interfaces
{
    public interface IUnitRepository
    {
        Task<List<Unit>> GetUnitsAsync();
        Task<Unit?> GetUnitByIdAsync(int unitId);
    }
}
