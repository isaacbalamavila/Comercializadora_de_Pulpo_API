using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Role;

namespace comercializadora_de_pulpo_api.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Response<List<RoleDTO>>> GetRolesAsync();
    }
}
