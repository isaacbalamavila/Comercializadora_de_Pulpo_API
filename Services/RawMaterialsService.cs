using AutoMapper;
using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.RawMaterials;
using comercializadora_de_pulpo_api.Repositories.Interfaces;
using comercializadora_de_pulpo_api.Services.Interfaces;

namespace comercializadora_de_pulpo_api.Services
{
    public class RawMaterialsService(IRawMaterialsRepository rawMaterialsRepository, IMapper mapper)
        : IRawMaterialsService
    {
        private readonly IRawMaterialsRepository _rawMaterialsRepository = rawMaterialsRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<RawMaterialDTO>>> GetRawMaterialsAsync()
        {
            var rawMaterials = await _rawMaterialsRepository.GetRawMaterialsAsync();
            return Response<List<RawMaterialDTO>>.Ok(
                _mapper.Map<List<RawMaterialDTO>>(rawMaterials)
            );
        }
    }
}
