using AutoMapper;
using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Units;
using comercializadora_de_pulpo_api.Repositories.Interfaces;
using comercializadora_de_pulpo_api.Services.Interfaces;

namespace comercializadora_de_pulpo_api.Services
{
    public class UnitService(IUnitRepository unitRepository, IMapper mapper) : IUnitService
    {
        private readonly IUnitRepository _unitRepository = unitRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<UnitDTO>>> GetUnitsAsync()
        {
            var units = await _unitRepository.GetUnitsAsync();

            return Response<List<UnitDTO>>.Ok(_mapper.Map<List<UnitDTO>>(units));
        }
    }
}
