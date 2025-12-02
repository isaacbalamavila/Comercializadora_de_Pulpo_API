using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Supplier;

namespace comercializadora_de_pulpo_api.Services.Interfaces
{
    public interface ISuppliersService
    {
        Task<Response<List<SupplierDTO>>> GetSuppliersAsync(bool status);
        Task<Response<Supplier>> GetSupplierByIdAsync(Guid supplierId);
        Task<Response<SupplierDTO>> CreateSupplierAsync(SupplierRequestDTO request);
        Task<Response<Supplier>> UpdateSupplierAsync(Guid supplierId, SupplierRequestDTO request);
        Task<Response<bool>> DeleteSupplierAsync(Guid supplierId);
        Task<Response<bool>> RestoreSupplierAsync(Guid supplierId);
    }
}
