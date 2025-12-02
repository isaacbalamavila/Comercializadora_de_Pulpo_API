using AutoMapper;
using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Role;
using comercializadora_de_pulpo_api.Repositories.Interfaces;
using comercializadora_de_pulpo_api.Services.Interfaces;

namespace comercializadora_de_pulpo_api.Services
{
    public class RoleService(IRolesRepository rolesRepository, IMapper mapper) : IRoleService
    {
        private readonly IRolesRepository _rolesRepository = rolesRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<RoleDTO>>> GetRolesAsync()
        {
            var roles = await _rolesRepository.GetRolesAsync();

            return Response<List<RoleDTO>>.Ok(_mapper.Map<List<RoleDTO>>(roles));
        }
    }
}
