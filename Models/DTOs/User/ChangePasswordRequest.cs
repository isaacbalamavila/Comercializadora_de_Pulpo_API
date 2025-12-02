using System.ComponentModel.DataAnnotations;

namespace comercializadora_de_pulpo_api.Models.DTOs.User
{
    public class ChangePasswordRequest
    {
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [RegularExpression(
            @"^(?=.*\d.*\d)(?=.*[!@#$%^&*(),.?""{}|<>_\-+=])(?=.*[A-Za-z]).{8,}$",
            ErrorMessage = "La contraseña debe tener al menos 8 caracteres, incluir 2 números y 1 carácter especial."
        )]
        public string NewPassword { get; set; } = null!;
    }
}
