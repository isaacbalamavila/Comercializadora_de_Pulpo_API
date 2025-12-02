using System.ComponentModel.DataAnnotations;

namespace comercializadora_de_pulpo_api.Models.DTOs.Products
{
    public class UpdateProductDTO
    {
        [Required(ErrorMessage = "Nombre obligatorio")]
        [RegularExpression(
            @"^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9\s]{3,150}$",
            ErrorMessage = "El nombre no puede contener carácteres especiales, con longitud entre 3 y 200 caracteres"
        )]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(
            255,
            MinimumLength = 10,
            ErrorMessage = "La descripción debe tener entre 10 y 255 caracteres"
        )]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "El precio es obligatorio")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "El stock mínimo es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El stock mínimo debe ser mayor que 0")]
        public int StockMin { get; set; }

        public IFormFile? Img { get; set; }
    }
}
