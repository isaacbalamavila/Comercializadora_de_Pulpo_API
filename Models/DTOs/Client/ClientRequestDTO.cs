using System.ComponentModel.DataAnnotations;

namespace comercializadora_de_pulpo_api.Models.DTOs.Client
{
    public class ClientRequestDTO
    {
        [Required(ErrorMessage = "Nombre obligatorio")]
        [RegularExpression(
            @"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{3,200}$",
            ErrorMessage = "El nombre solo puede contener letras y espacios, con longitud entre 3 y 200 caracteres"
        )]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Teléfono obligatorio")]
        [StringLength(10, ErrorMessage = "El teléfono debe contener máximo 10 dígitos")]
        [RegularExpression(@"^\d{1,10}$", ErrorMessage = "El teléfono debe contener solo números")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Correo electrónico obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        [StringLength(
            100,
            MinimumLength = 10,
            ErrorMessage = "El correo electrónico debe tener entre 10 y 100 caracteres"
        )]
        public string Email { get; set; } = null!;

        [StringLength(
            13,
            MinimumLength = 12,
            ErrorMessage = "El RFC debe tener entre 12 y 13 caracteres"
        )]
        public string? Rfc { get; set; }
    }
}
