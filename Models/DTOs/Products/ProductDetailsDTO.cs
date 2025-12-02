namespace comercializadora_de_pulpo_api.Models.DTOs.Products
{
    public class ProductDetailsDTO
    {
        public Guid Id { get; set; }

        public string Sku { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Img { get; set; } = null!;

        public int RawMaterialId { get; set; }
        public string RawMaterial { get; set; } = null!;

        public int Content { get; set; }

        public int UnitId { get; set; }
        public string Unit { get; set; } = null!;

        public decimal Price { get; set; }

        public int StockMin { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal RawMaterialNeededKg { get; set; }

        public int TimeNeededMin { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
