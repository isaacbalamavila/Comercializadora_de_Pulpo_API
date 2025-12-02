namespace comercializadora_de_pulpo_api.Models.DTOs.Units
{
    public class UnitDTO
    {
        public int Id { get; set; }

        public string Abbreviation { get; set; } = null!;

        public string? Label { get; set; }
    }
}
