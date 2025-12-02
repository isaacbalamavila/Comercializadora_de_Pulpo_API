using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Client;
using comercializadora_de_pulpo_api.Services;
using comercializadora_de_pulpo_api.Services.Interfaces;
using comercializadora_de_pulpo_api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace comercializadora_de_pulpo_api.Controllers
{
    [Route("/clients")]
    [ApiController]
    [Authorize]
    public class ClientsController(IClientsService clientsService) : ControllerBase
    {
        private readonly IClientsService _clientService = clientsService;

        private IActionResult HandleResponse<T>(Response<T> response, object? successData = null)
        {
            if (response.IsSuccess)
                return response.StatusCode switch
                {
                    204 => NoContent(),
                    201 => StatusCode(201, successData ?? response.Data),
                    _ => Ok(successData ?? response.Data),
                };

            return response.StatusCode switch
            {
                400 => BadRequest(response.Error),
                404 => NotFound(response.Error),
                _ => StatusCode(500, response.Error),
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetClients([FromQuery] bool? onlyActives, Guid? idExcluded)
        {
            return HandleResponse(
                await _clientService.GetClientsAsync(onlyActives ?? false, idExcluded)
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetClientDetails([FromRoute] Guid id)
        {
            return HandleResponse(await _clientService.GetClientByIdAsync(id));
        }

        [HttpPost]
        [Authorize(Policy = RoleAccess.ADMINORMANAGER)]
        public async Task<IActionResult> CreateClient([FromBody] ClientRequestDTO newClient)
        {
            return HandleResponse(await _clientService.CreateClientAsync(newClient));
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Policy = RoleAccess.ADMINORMANAGER)]
        public async Task<IActionResult> UpdateClient(Guid id, ClientRequestDTO request)
        {
            return HandleResponse(await _clientService.UpdateClientAsync(id, request));
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = RoleAccess.ADMINORMANAGER)]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            return HandleResponse(await _clientService.DeleteClientAsync(id));
        }

        [HttpPatch("{id:Guid}/restore")]
        [Authorize(Policy = RoleAccess.ADMINORMANAGER)]
        public async Task<IActionResult> RestoreSupplier(Guid id)
        {
            return HandleResponse(await _clientService.RestoreClientAsync(id));
        }
    }
}
