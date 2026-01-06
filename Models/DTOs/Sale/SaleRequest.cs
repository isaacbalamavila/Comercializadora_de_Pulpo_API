using System.ComponentModel.DataAnnotations;

namespace comercializadora_de_pulpo_api.Models.DTOs.Sale
{
    public class SaleRequest
    {
        [Required(ErrorMessage ="El ID del usuario es obligatorio")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage ="El ID del cliente es obligatorio")]
        public Guid ClientId { get; set; }
        [Required(ErrorMessage ="El ID del método de pago es obligatorio")]
        public int PaymentMethodId { get; set; }
        public List<SaleItem> Products { get; set; } = [];
    }

    public class SaleItem { 
        public Guid ProductId {get; set;}
        public int Quantity { get; set;}
    }
}
