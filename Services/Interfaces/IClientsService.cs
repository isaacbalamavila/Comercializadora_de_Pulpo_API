using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Client;

namespace comercializadora_de_pulpo_api.Services.Interfaces
{
    public interface IClientsService
    {
        Task<Response<List<ClientDTO>>> GetClientsAsync(bool onlyActives, Guid? idExcluded);
        Task<Response<Client>> GetClientByIdAsync(Guid clientId);
        Task<Response<ClientDTO>> CreateClientAsync(ClientRequestDTO request);
        Task<Response<Client>> UpdateClientAsync(Guid clientId, ClientRequestDTO request);
        Task<Response<bool>> DeleteClientAsync(Guid clientId);
        Task<Response<bool>> RestoreClientAsync(Guid clientId);
    }
}
