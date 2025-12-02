using AutoMapper;
using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Client;
using comercializadora_de_pulpo_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace comercializadora_de_pulpo_api.Repositories
{
    public class ClientRepository(ComercializadoraDePulpoContext context, IMapper mapper)
        : IClientsRepository
    {
        private readonly ComercializadoraDePulpoContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<Client>> GetClientsAsync(bool onlyActives, Guid? idExcluded)
        {
            var query = _context.Clients.AsQueryable();

            if (onlyActives)
                query = query.Where(cl => !cl.IsDeleted);

            if (idExcluded.HasValue)
                query = query.Where(cl => cl.Id != idExcluded.Value);

            query = query.OrderByDescending(cl => cl.CreatedAt);

            return await query.ToListAsync();
        }

        public async Task<Client?> GetClientByIdAsync(Guid id)
        {
            return await _context.Clients.SingleOrDefaultAsync(cl => cl.Id == id);
        }

        public async Task<bool> VerifyEmailAsync(string? email)
        {
            if (String.IsNullOrEmpty(email))
                return true;

            return !await _context.Clients.AnyAsync(cl => cl.Email == email);
        }

        public async Task<bool> VerifyPhoneAsync(string? phone)
        {
            if (String.IsNullOrEmpty(phone))
                return true;

            return !await _context.Clients.AnyAsync(cl => cl.Phone == phone);
        }

        public async Task<bool> VerifyRFCAsync(string? RFC)
        {
            return RFC == null || !await (_context.Clients.AnyAsync(cl => cl.Rfc == RFC.ToUpper()));
        }

        public async Task<Response<ClientDTO>> CreateClientAsync(Client newClient)
        {
            try
            {
                await _context.Clients.AddAsync(newClient);
                await _context.SaveChangesAsync();

                return Response<ClientDTO>.Ok(_mapper.Map<ClientDTO>(newClient), 201);
            }
            catch (Exception ex)
            {
                return Response<ClientDTO>.Fail("Error al crear el cliente", ex.Message);
            }
        }

        public async Task<Response<Client>> UpdateClientAsync(Client updatedClient)
        {
            try
            {
                _context.Clients.Update(updatedClient);
                await _context.SaveChangesAsync();
                return Response<Client>.Ok(updatedClient);
            }
            catch (Exception ex)
            {
                return Response<Client>.Fail("Error al intentar actualizar el cliente", ex.Message);
            }
        }
    }
}
