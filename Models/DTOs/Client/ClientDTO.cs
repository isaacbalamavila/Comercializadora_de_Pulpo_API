namespace comercializadora_de_pulpo_api.Models.DTOs.Client
{
    public class ClientDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
