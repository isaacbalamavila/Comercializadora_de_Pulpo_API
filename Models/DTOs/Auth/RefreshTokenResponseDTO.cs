namespace comercializadora_de_pulpo_api.Models.DTOs.Login
{
    public class RefreshTokenResponseDTO
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
    }
}
