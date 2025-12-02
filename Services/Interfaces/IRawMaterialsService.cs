using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.RawMaterials;

namespace comercializadora_de_pulpo_api.Services.Interfaces
{
    public interface IRawMaterialsService
    {
        Task<Response<List<RawMaterialDTO>>> GetRawMaterialsAsync();
    }
}
