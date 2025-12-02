using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Supplier;

namespace comercializadora_de_pulpo_api.Repositories.Interfaces
{
    public interface ISuppliersRepository
    {
        Task<List<Supplier>> GetSuppliersAsync(bool onlyActives);
        Task<Supplier?> GetSupplierByIdAsync(Guid supplierId);
        Task<bool> VerifyEmailAsync(string? email);
        Task<bool> VerifyPhoneAsync(string? phone);
        Task<bool> VerifyNameAsync(string name);
        Task<bool> VerifyRFCAsync(string? rfc);
        Task<Response<Supplier>> CreateSupplierAsync(Supplier newSupplier);
        Task<Response<Supplier>> UpdateSupplierAsync(Supplier updatedSupplier);
    }
}
