using AutoMapper;
using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Supplier;
using comercializadora_de_pulpo_api.Models.DTOs.User;
using comercializadora_de_pulpo_api.Repositories;
using comercializadora_de_pulpo_api.Repositories.Interfaces;
using comercializadora_de_pulpo_api.Services.Interfaces;

namespace comercializadora_de_pulpo_api.Services
{
    public class SuppliersService(ISuppliersRepository suppliersRepository, IMapper mapper)
        : ISuppliersService
    {
        private readonly ISuppliersRepository _supplierRepository = suppliersRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<SupplierDTO>>> GetSuppliersAsync(bool onlyActives)
        {
            var suppliers = await _supplierRepository.GetSuppliersAsync(onlyActives);
            return Response<List<SupplierDTO>>.Ok(_mapper.Map<List<SupplierDTO>>(suppliers));
        }

        public async Task<Response<Supplier>> GetSupplierByIdAsync(Guid supplierId)
        {
            var supplier = await _supplierRepository.GetSupplierByIdAsync(supplierId);

            return supplier == null
                ? Response<Supplier>.Fail(
                    "Proveedor no encontrado",
                    $"No se encontró ningún proveedor con el ID '{supplierId}'",
                    404
                )
                : Response<Supplier>.Ok(supplier);
        }

        public async Task<Response<SupplierDTO>> CreateSupplierAsync(SupplierRequestDTO request)
        {
            request.Name = request.Name.Trim().ToLower();
            request.Email = request.Email.Trim().ToLower();
            request.AltEmail = !String.IsNullOrEmpty(request.AltEmail)
                ? request.AltEmail.Trim().ToLower()
                : null;
            request.Rfc = !String.IsNullOrEmpty(request.Rfc) ? request.Rfc.Trim().ToUpper() : null;

            if (request.Phone == request.AltPhone)
            {
                return Response<SupplierDTO>.Fail(
                    "El teléfono principal y el teléfono alternativo son iguales",
                    $"El telefono principal '{request.Phone}' es igual al alternativo '{request.Phone}'",
                    400
                );
            }

            if (request.Email == request.AltEmail)
            {
                return Response<SupplierDTO>.Fail(
                    "El correo principal y el correo alternativo son iguales",
                    $"El correo principal '{request.Email}' es igual al alternativo '{request.AltEmail}'",
                    400
                );
            }

            if (!await _supplierRepository.VerifyNameAsync(request.Name))
            {
                return Response<SupplierDTO>.Fail(
                    "Nombre ya registrado",
                    $"El nombre '{request.Name}' ya existe en el sistema",
                    400
                );
            }
            if (!await _supplierRepository.VerifyEmailAsync(request.Email))
                return Response<SupplierDTO>.Fail(
                    "Correo electrónico ya registrado",
                    $"El correo '{request.Email}' ya existe en el sistema",
                    400
                );

            if (!await _supplierRepository.VerifyEmailAsync(request.AltEmail))
                return Response<SupplierDTO>.Fail(
                    "Correo electrónico alternativo ya registrado",
                    $"El correo '{request.AltEmail}' ya existe en el sistema",
                    400
                );

            if (!await _supplierRepository.VerifyPhoneAsync(request.Phone))
                return Response<SupplierDTO>.Fail(
                    "Teléfono ya registrado",
                    $"El teléfono '{request.Phone}' ya existe en el sistema",
                    400
                );

            if (!await _supplierRepository.VerifyPhoneAsync(request.AltPhone))
                return Response<SupplierDTO>.Fail(
                    "Teléfono alternativo ya registrado",
                    $"El teléfono '{request.AltPhone}' ya existe en el sistema.",
                    400
                );

            if (request.Rfc != null && !await _supplierRepository.VerifyRFCAsync(request.Rfc))
                return Response<SupplierDTO>.Fail(
                    "RFC ya registrado",
                    $"El RFC '{request.Rfc}' ya existe en el sistema.",
                    400
                );

            Supplier newSupplier = _mapper.Map<Supplier>(request);

            newSupplier.Id = Guid.NewGuid();
            newSupplier.CreatedAt = DateTime.Now;
            newSupplier.IsDeleted = false;

            var createRequest = await _supplierRepository.CreateSupplierAsync(newSupplier);

            return createRequest.IsSuccess
                ? Response<SupplierDTO>.Ok(_mapper.Map<SupplierDTO>(newSupplier), 201)
                : Response<SupplierDTO>.Fail(
                    "Ocurrió un error al intentar crear el proveedor",
                    createRequest.Error!.ErrorDetails
                );
        }

        public async Task<Response<Supplier>> UpdateSupplierAsync(
            Guid supplierId,
            SupplierRequestDTO request
        )
        {
            request.Name = request.Name.Trim().ToLower();
            request.Email = request.Email.Trim().ToLower();
            request.AltEmail = !String.IsNullOrEmpty(request.AltEmail)
                ? request.AltEmail.Trim().ToLower()
                : null;
            request.Rfc = !String.IsNullOrEmpty(request.Rfc) ? request.Rfc.Trim().ToUpper() : null;

            if (request.Phone == request.AltPhone)
                return Response<Supplier>.Fail(
                    "El teléfono principal y el teléfono alternativo son iguales",
                    $"El telefono principal '{request.Phone}' es igual al alternativo '{request.Phone}'",
                    400
                );

            if (request.Email == request.AltEmail)
                return Response<Supplier>.Fail(
                    "El correo principal y el correo alternativo son iguales",
                    $"El correo principal '{request.Email}' es igual al alternativo '{request.AltEmail}'",
                    400
                );

            var supplierSaved = await _supplierRepository.GetSupplierByIdAsync(supplierId);

            if (supplierSaved == null)
                return Response<Supplier>.Fail(
                    "El usuario no existe",
                    $"El usuario con el ID '{supplierId}' no existe",
                    404
                );

            var supplier = supplierSaved!;

            if (supplier.IsDeleted)
                return Response<Supplier>.Fail(
                    "No se puede modificar un proveedor eliminado",
                    $"El proveedor con el ID '{supplierId}' se encuentra eliminado y con las modificaciones restringidas",
                    404
                );

            if (
                !supplier.Name.Equals(request.Name, StringComparison.CurrentCultureIgnoreCase)
                && !await _supplierRepository.VerifyNameAsync(request.Name)
            )
                return Response<Supplier>.Fail(
                    "Nombre ya registrado",
                    $"El nombre '{request.Name}' ya existe en el sistema",
                    400
                );

            if (
                supplier.Email != request.Email
                && !await _supplierRepository.VerifyEmailAsync(request.Email)
            )
                return Response<Supplier>.Fail(
                    "Correo electrónico ya registrado",
                    $"El correo '{request.Email}' ya existe en el sistema",
                    400
                );

            if (
                supplier.AltEmail != request.AltEmail
                && !await _supplierRepository.VerifyEmailAsync(request.AltEmail)
            )
                return Response<Supplier>.Fail(
                    "Correo electrónico alternativo ya registrado",
                    $"El correo '{request.Email}' ya existe en el sistema",
                    400
                );

            if (
                supplier.Phone != request.Phone
                && !await _supplierRepository.VerifyPhoneAsync(request.Phone)
            )
                return Response<Supplier>.Fail(
                    "Número de teléfono ya registrado",
                    $"El número '{request.Phone}' ya existe en el sistema",
                    400
                );

            if (
                supplier.AltPhone != request.AltPhone
                && !await _supplierRepository.VerifyPhoneAsync(request.AltPhone)
            )
                return Response<Supplier>.Fail(
                    "Teléfono alternativo ya registrado",
                    $"El teléfono '{request.AltPhone}' ya existe en el sistema.",
                    400
                );

            if (
                supplier.Rfc != null
                && supplier.Rfc != request.Rfc
                && !await _supplierRepository.VerifyRFCAsync(request.Rfc)
            )
                return Response<Supplier>.Fail(
                    "RFC ya registrado",
                    $"El RFC '{request.Rfc}' ya existe en el sistema.",
                    400
                );

            //* Update Fields
            supplierSaved.Name = request.Name;
            supplierSaved.Email = request.Email;
            supplierSaved.AltEmail = request.AltEmail;
            supplierSaved.Phone = request.Phone;
            supplierSaved.AltPhone = request.AltPhone;
            supplierSaved.Rfc = request.Rfc;

            return await _supplierRepository.UpdateSupplierAsync(supplierSaved);
        }

        public async Task<Response<bool>> DeleteSupplierAsync(Guid supplierId)
        {
            var supplierSaved = await _supplierRepository.GetSupplierByIdAsync(supplierId);

            if (supplierSaved == null)
                return Response<bool>.Fail(
                    "Proveedor no encontrado",
                    $"No se encontró ningún proveedor con el ID '{supplierId}'",
                    404
                );

            if (supplierSaved.IsDeleted)
                return Response<bool>.Ok(true, 204);

            supplierSaved.IsDeleted = true;

            var updateRequest = await _supplierRepository.UpdateSupplierAsync(supplierSaved);

            return updateRequest.IsSuccess
                ? Response<bool>.Ok(true, 204)
                : Response<bool>.Fail(
                    "Ocurrió un error al intentar eliminar el proveedor",
                    updateRequest.Error!.ErrorDetails,
                    500
                );
        }

        public async Task<Response<bool>> RestoreSupplierAsync(Guid supplierId)
        {
            var supplierSaved = await _supplierRepository.GetSupplierByIdAsync(supplierId);

            if (supplierSaved == null)
                return Response<bool>.Fail(
                    "Proveedor no encontrado",
                    $"No se encontró ningún proveedor con el ID '{supplierId}'",
                    404
                );

            if (!supplierSaved.IsDeleted)
                return Response<bool>.Ok(true);

            supplierSaved.IsDeleted = false;

            var updateRequest = await _supplierRepository.UpdateSupplierAsync(supplierSaved);

            return updateRequest.IsSuccess
                ? Response<bool>.Ok(true)
                : Response<bool>.Fail(
                    "Ocurrió un error al intentar restaurar el proveedor",
                    updateRequest.Error!.ErrorDetails,
                    500
                );
        }
    }
}
