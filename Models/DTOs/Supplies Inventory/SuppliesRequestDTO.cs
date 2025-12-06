using comercializadora_de_pulpo_api.Models.DTOs.Pagination;

namespace comercializadora_de_pulpo_api.Models.DTOs.Supplies_Inventory
{
    public class SuppliesRequestDTO  :PaginationRequest
    {
        public string? Search { get; set; }
        public decimal? RemainKg { get; set; }

        public int? RawMaterialId { get; set; }
    }
}
