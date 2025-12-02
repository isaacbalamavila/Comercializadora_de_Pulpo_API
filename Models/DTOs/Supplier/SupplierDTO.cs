namespace comercializadora_de_pulpo_api.Models.DTOs.Supplier
{
    public class SupplierDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Rfc { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
