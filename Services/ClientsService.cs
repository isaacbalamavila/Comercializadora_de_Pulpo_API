using AutoMapper;
using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Client;
using comercializadora_de_pulpo_api.Models.DTOs.Supplier;
using comercializadora_de_pulpo_api.Repositories;
using comercializadora_de_pulpo_api.Repositories.Interfaces;
using comercializadora_de_pulpo_api.Services.Interfaces;

namespace comercializadora_de_pulpo_api.Services
{
    public class ClientsService(IClientsRepository clientsRepository, IMapper mapper)
        : IClientsService
    {
        private readonly IClientsRepository _clientRepository = clientsRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<ClientDTO>>> GetClientsAsync(
            bool onlyActives,
            Guid? idExcluded
        )
        {
            var clients = await _clientRepository.GetClientsAsync(onlyActives, idExcluded);

            return Response<List<ClientDTO>>.Ok(_mapper.Map<List<ClientDTO>>(clients));
        }

        public async Task<Response<Client>> GetClientByIdAsync(Guid clientId)
        {
            var client = await _clientRepository.GetClientByIdAsync(clientId);

            return client == null
                ? Response<Client>.Fail(
                    "Cliente no encontrado",
                    $"No se encontró un cliente con el ID '{clientId}'",
                    404
                )
                : Response<Client>.Ok(client);
        }

        public async Task<Response<ClientDTO>> CreateClientAsync(ClientRequestDTO request)
        {
            request.Name = request.Name.Trim().ToLower();
            request.Email = request.Email.Trim().ToLower();
            request.Rfc = !String.IsNullOrEmpty(request.Rfc) ? request.Rfc.Trim().ToUpper() : null;

            if (!await _clientRepository.VerifyEmailAsync(request.Email))
                return Response<ClientDTO>.Fail(
                    "Correo electrónico ya registrado",
                    $"El correo '{request.Email}' ya existe en el sistema",
                    400
                );

            if (request.Rfc != null && !await _clientRepository.VerifyRFCAsync(request.Rfc))
                return Response<ClientDTO>.Fail(
                    "RFC ya registrado",
                    $"El RFC '{request.Rfc}' ya existe en el sistema",
                    400
                );

            Client newClient = _mapper.Map<Client>(request);

            newClient.Id = Guid.NewGuid();
            newClient.CreatedAt = DateTime.UtcNow;
            newClient.IsDeleted = false;

            return await _clientRepository.CreateClientAsync(newClient);
        }

        public async Task<Response<Client>> UpdateClientAsync(
            Guid clientId,
            ClientRequestDTO request
        )
        {
            request.Name = request.Name.Trim().ToLower();
            request.Email = request.Email.Trim().ToLower();
            request.Rfc = !String.IsNullOrEmpty(request.Rfc) ? request.Rfc.Trim().ToUpper() : null;

            var clientSaved = await _clientRepository.GetClientByIdAsync(clientId);

            if (clientSaved == null)
                return Response<Client>.Fail(
                    "Cliente no encontrado",
                    $"No se encontró un cliente con el ID '{clientId}'",
                    404
                );

            if (clientSaved.IsDeleted)
                return Response<Client>.Fail(
                    "No se puede modificar un cliente eliminado",
                    $"El usuario con el ID: '{clientId}' se encuentra eliminado y con las modificaciones restringidas",
                    400
                );

            if (
                !clientSaved.Email.Equals(request.Email, StringComparison.CurrentCultureIgnoreCase)
                && !await _clientRepository.VerifyEmailAsync(request.Email)
            )
                return Response<Client>.Fail(
                    "Correo electrónico ya registrado",
                    $"El correo electrónico '{request.Email}' ya existe en el sistema",
                    400
                );
            if (
                request.Phone != clientSaved.Phone
                && !await _clientRepository.VerifyPhoneAsync(request.Phone)
            )
                return Response<Client>.Fail(
                    "Teléfono ya registrado",
                    $"El teléfono '{request.Phone}' ya existe en el sistema",
                    400
                );

            if (
                request.Rfc != clientSaved.Rfc
                && !await _clientRepository.VerifyRFCAsync(request.Rfc)
            )
                return Response<Client>.Fail(
                    "RFC ya registrado",
                    $"El RFC '{request.Rfc}' ya existe en el sistema",
                    400
                );

            // Update Fields
            clientSaved.Email = request.Email;
            clientSaved.Name = request.Name;
            clientSaved.Phone = request.Phone;
            clientSaved.Rfc = request.Rfc;

            return await _clientRepository.UpdateClientAsync(clientSaved);
        }

        public async Task<Response<bool>> DeleteClientAsync(Guid clientId)
        {
            var clientSaved = await _clientRepository.GetClientByIdAsync(clientId);

            if (clientSaved == null)
                return Response<bool>.Fail(
                    "Cliente no encontrado",
                    $"No se encontró un cliente con el ID '{clientId}'",
                    404
                );

            if (clientSaved.IsDeleted)
                return Response<bool>.Ok(true, 204);

            clientSaved.IsDeleted = true;

            var updateRequest = await _clientRepository.UpdateClientAsync(clientSaved);

            return updateRequest.IsSuccess
                ? Response<bool>.Ok(true, 204)
                : Response<bool>.Fail(
                    "Ocurrió un error al intentar eliminar al cliente",
                    updateRequest.Error!.ErrorDetails
                );
        }

        public async Task<Response<bool>> RestoreClientAsync(Guid clientId)
        {
            var clientSaved = await _clientRepository.GetClientByIdAsync(clientId);

            if (clientSaved == null)
                return Response<bool>.Fail(
                    "Cliente no encontrado",
                    $"No se encontró un cliente con el ID '{clientId}'",
                    404
                );

            if (!clientSaved.IsDeleted)
                return Response<bool>.Ok(true);

            clientSaved.IsDeleted = false;

            var updateRequest = await _clientRepository.UpdateClientAsync(clientSaved);

            return updateRequest.IsSuccess
                ? Response<bool>.Ok(true)
                : Response<bool>.Fail(
                    "Ocurrió un error al intentar restaurar al cliente",
                    updateRequest.Error!.ErrorDetails
                );
        }
    }
}
