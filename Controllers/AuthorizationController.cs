using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Login;
using comercializadora_de_pulpo_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace comercializadora_de_pulpo_api.Controllers
{
    [Route("/authorization")]
    [ApiController]
    public class AuthorizationController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        private IActionResult HandleResponse<T>(Response<T> response, object? successData = null)
        {
            if (response.IsSuccess)
                return Ok(successData ?? response.Data);

            return response.StatusCode switch
            {
                400 => BadRequest(response.Error),
                401 => Unauthorized(response.Error),
                404 => NotFound(response.Error),
                _ => StatusCode(500, response.Error),
            };
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO body)
        {
            return HandleResponse(await _authService.LoginAsync(body));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO body)
        {
            return HandleResponse(await _authService.RefreshAccessTokenAsync(body));
        }
    }
}
