using System.Threading.Tasks;
using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Client;

namespace comercializadora_de_pulpo_api.Repositories.Interfaces
{
    public interface IClientsRepository
    {
        Task<List<Client>> GetClientsAsync(bool onlyActives, Guid? idExcluded);
        Task<Client?> GetClientByIdAsync(Guid clientId);
        Task<bool> VerifyEmailAsync(string? email);
        Task<bool> VerifyPhoneAsync(string? phone);
        Task<bool> VerifyRFCAsync(string? RFC);
        Task<Response<ClientDTO>> CreateClientAsync(Client newClient);
        Task<Response<Client>> UpdateClientAsync(Client updatedClient);
    }
}
