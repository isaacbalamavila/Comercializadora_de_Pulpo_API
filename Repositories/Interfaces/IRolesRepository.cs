using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Role;

namespace comercializadora_de_pulpo_api.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        Task<List<Role>> GetRolesAsync();
        Task<Role?> GetRoleById(int id);
    }
}
