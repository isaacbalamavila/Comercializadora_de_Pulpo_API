namespace comercializadora_de_pulpo_api.Models.DTOs.Login
{
    public class LoginResponseDTO
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; } = null!;
        public required string UserName { get; set; } = null!;
        public required string UserLastName { get; set; } = null!;
        public required string Role { get; set; } = null!;
        public required int RoleId { get; set; }
        public required string AccessToken { get; set; } = null!;
        public required DateTime AccessTokenExpiratesAt { get; set; }
        public required string RefreshToken { get; set; } = null!;
        public required DateTime RefreshTokenExpiresAt { get; set; }
        public required bool FirstLogin { get; set; }
    }
}
