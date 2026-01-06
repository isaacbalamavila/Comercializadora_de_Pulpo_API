using System.ComponentModel.DataAnnotations;

namespace comercializadora_de_pulpo_api.Models.DTOs.Purchases
{
    public class CreatePurchaseDTO
    {
        [Required(ErrorMessage = "El ID del usuario es obligatorio")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "El ID del proveedor es obligatorio")]
        public Guid SupplierId { get; set; }

        [Required(ErrorMessage = "El ID de la materia prima es obligatoria")]
        public int RawMaterialId { get; set; }

        [Required(ErrorMessage = "El total de kilogramos comprados es obligatorio")]
        public decimal TotalKg { get; set; }

        [Required(ErrorMessage = "El precio total es obligatorio")]
        public decimal TotalPrice { get; set; }
    }
}
