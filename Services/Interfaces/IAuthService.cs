using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Login;

namespace comercializadora_de_pulpo_api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Response<LoginResponseDTO>> LoginAsync(LoginRequestDTO request);
        Task<Response<RefreshTokenResponseDTO>> RefreshAccessTokenAsync(RefreshTokenRequestDTO request);
    }
}
