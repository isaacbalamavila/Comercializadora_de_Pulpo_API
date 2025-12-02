using System.ComponentModel.DataAnnotations;

namespace comercializadora_de_pulpo_api.Models.DTOs.User
{
    public class UpdateUserDTO
    {
        [Required(ErrorMessage = "Correo electrónico obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        [StringLength(
            100,
            MinimumLength = 10,
            ErrorMessage = "El correo electrónico debe tener entre 10 y 100 caracteres"
        )]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Nombre obligatorio")]
        [RegularExpression(
            @"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{3,50}$",
            ErrorMessage = "El nombre solo puede contener letras y espacios, con longitud entre 3 y 50 caracteres"
        )]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Apellido obligatorio")]
        [RegularExpression(
            @"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{2,50}$",
            ErrorMessage = "El apellido solo puede contener letras y espacios, con longitud entre 3 y 50 caracteres"
        )]
        public string LastName { get; set; } = null!;

        [StringLength(10, ErrorMessage = "El teléfono debe contener máximo 10 dígitos")]
        [RegularExpression(@"^\d{1,10}$", ErrorMessage = "El teléfono debe contener solo números")]
        public string? Phone { get; set; }
    }
}
