using AutoMapper;
using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Supplier;
using comercializadora_de_pulpo_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace comercializadora_de_pulpo_api.Repositories
{
    public class SuppliersRepository(ComercializadoraDePulpoContext context, IMapper mapper)
        : ISuppliersRepository
    {
        private readonly ComercializadoraDePulpoContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<Supplier>> GetSuppliersAsync(bool onlyActives)
        {
            var query = _context.Suppliers.AsQueryable();

            if (onlyActives)
                query = query.Where(sp => !sp.IsDeleted);

            query = query.OrderByDescending(cl => cl.CreatedAt);

            return await query.ToListAsync();
        }

        public async Task<Supplier?> GetSupplierByIdAsync(Guid supplierId)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == supplierId);
        }

        public async Task<bool> VerifyEmailAsync(string? email)
        {
            if (String.IsNullOrWhiteSpace(email))
                return true;

            return !await _context.Suppliers.AnyAsync(s => s.Email == email || s.AltEmail == email);
        }

        public async Task<bool> VerifyPhoneAsync(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return true;

            return !await _context.Suppliers.AnyAsync(s => s.Phone == phone || s.AltPhone == phone);
        }

        public async Task<bool> VerifyNameAsync(string name)
        {
            return !await _context.Suppliers.AnyAsync(s => s.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> VerifyRFCAsync(string? RFC)
        {
            return RFC == null || !await (_context.Suppliers.AnyAsync(s => s.Rfc == RFC.ToUpper()));
        }

        public async Task<Response<Supplier>> CreateSupplierAsync(Supplier newSupplier)
        {
            try
            {
                await _context.Suppliers.AddAsync(newSupplier);
                await _context.SaveChangesAsync();

                return Response<Supplier>.Ok(newSupplier, 201);
            }
            catch (Exception ex)
            {
                return Response<Supplier>.Fail("Error al intentar crear el proveedor", ex.Message);
            }
        }

        public async Task<Response<Supplier>> UpdateSupplierAsync(Supplier updatedSupplier)
        {
            try
            {
                _context.Suppliers.Update(updatedSupplier);

                await _context.SaveChangesAsync();

                return Response<Supplier>.Ok(updatedSupplier);
            }
            catch (Exception ex)
            {
                return Response<Supplier>.Fail(
                    "Error al intentar actualizar el proveedor",
                    ex.Message
                );
            }
        }
    }
}
