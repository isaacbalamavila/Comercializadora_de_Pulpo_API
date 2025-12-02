namespace comercializadora_de_pulpo_api.Models.DTOs.Products
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Sku { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public string Img { get; set; } = null!;
        public int RawMaterialId { get; set; }
        public string RawMaterial { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
