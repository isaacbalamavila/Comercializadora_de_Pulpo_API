using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace comercializadora_de_pulpo_api.Controllers
{
    [Route("/raw-materials")]
    [Authorize]
    [ApiController]
    public class RawMaterialsController(IRawMaterialsService materialService) : ControllerBase
    {
        private readonly IRawMaterialsService _materialService = materialService;

        private IActionResult HandleResponse<T>(Response<T> response, object? successData = null)
        {
            if (response.IsSuccess)
                return response.StatusCode switch
                {
                    204 => NoContent(),
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
        public async Task<IActionResult> GetRawsMaterials()
        {
            return HandleResponse(await _materialService.GetRawMaterialsAsync());
        }
    }
}
