using System.ComponentModel.DataAnnotations;

namespace comercializadora_de_pulpo_api.Models.DTOs.User
{
    public class UserDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public int RoleId { get; set; }

        [Required]
        public string Role { get; set; } = null!;

        public string? Phone { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
