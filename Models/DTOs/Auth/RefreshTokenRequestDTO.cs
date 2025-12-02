using System.ComponentModel.DataAnnotations;

namespace comercializadora_de_pulpo_api.Models.DTOs.Login
{
    public class RefreshTokenRequestDTO
    {
        [Required(ErrorMessage = "El Token es obligatorio")]
        public string RefreshToken { get; set; } = null!;
    }
}
