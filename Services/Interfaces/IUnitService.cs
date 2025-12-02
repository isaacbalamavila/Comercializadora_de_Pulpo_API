using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Units;

namespace comercializadora_de_pulpo_api.Services.Interfaces
{
    public interface IUnitService
    {
        Task<Response<List<UnitDTO>>> GetUnitsAsync();
    }
}
