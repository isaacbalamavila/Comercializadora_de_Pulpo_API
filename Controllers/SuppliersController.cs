using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Supplier;
using comercializadora_de_pulpo_api.Models.DTOs.User;
using comercializadora_de_pulpo_api.Services;
using comercializadora_de_pulpo_api.Services.Interfaces;
using comercializadora_de_pulpo_api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace comercializadora_de_pulpo_api.Controllers
{
    [Route("/suppliers")]
    [Authorize]
    [ApiController]
    public class SuppliersController(ISuppliersService suppliersService) : ControllerBase
    {
        private readonly ISuppliersService _supplierService = suppliersService;

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
        public async Task<IActionResult> GetSuppliers([FromQuery] bool? onlyActives)
        {
            return HandleResponse(await _supplierService.GetSuppliersAsync(onlyActives ?? false));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetSupplierDetails([FromRoute] Guid id)
        {
            return HandleResponse(await _supplierService.GetSupplierByIdAsync(id));
        }

        [HttpPost]
        [Authorize(Policy = RoleAccess.ADMINORMANAGER)]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierRequestDTO request)
        {
            return HandleResponse(await _supplierService.CreateSupplierAsync(request));
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Policy = RoleAccess.ADMINORMANAGER)]
        public async Task<IActionResult> UpdateSupplier(Guid id, [FromBody] SupplierRequestDTO body)
        {
            return HandleResponse(await _supplierService.UpdateSupplierAsync(id, body));
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = RoleAccess.ADMINORMANAGER)]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            return HandleResponse(await _supplierService.DeleteSupplierAsync(id));
        }

        [HttpPatch("{id:Guid}/restore")]
        [Authorize(Policy = RoleAccess.ADMINORMANAGER)]
        public async Task<IActionResult> RestoreSupplier(Guid id)
        {
            return HandleResponse(await _supplierService.RestoreSupplierAsync(id));
        }
    }
}
