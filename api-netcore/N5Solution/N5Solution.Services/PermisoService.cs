using N5Solution.Core.Entities;
using N5Solution.Core.Interfaces;
using N5Solution.Core.Interfaces.Services;
using N5Solution.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N5Solution.Services
{
    public class PermisoService : IPermisoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PermisoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Permiso> CreatePermiso(Permiso newPermiso)
        {
            PermisoValidator validator = new();

            var validationResult = await validator.ValidateAsync(newPermiso);

            if (!validationResult.IsValid)
            {
                var messageValidation = validationResult.Errors.FirstOrDefault();

                throw new ArgumentException(messageValidation.ToString());
            }

            var tipoPermiso = await _unitOfWork.TipoPermisoRepository.GetByIdAsync(newPermiso.IdTipoPermiso);

            if (tipoPermiso == null)
            {
                throw new ArgumentException("El tipo permiso enviado no es válido");
            }

            await _unitOfWork.PermisoRepository.AddAsync(newPermiso);
            await _unitOfWork.CommitAsync();
                        

            return newPermiso;
        }

        public async Task<IEnumerable<Permiso>> GetAll()
        {
            return await _unitOfWork.PermisoRepository.GetAllAsync();
        }

        public async Task<Permiso> GetPermisoById(int Id)
        {
            return await _unitOfWork.PermisoRepository.GetByIdAsync(Id);
        }

        public async Task<Permiso> UpdatePermiso(int permisoUpdatedId, Permiso permisoUpdated)
        {
            PermisoValidator validator = new();

            var validationResult = await validator.ValidateAsync(permisoUpdated);

            if (!validationResult.IsValid)
            {
                var messageValidation = validationResult.Errors.FirstOrDefault();

                throw new ArgumentException(messageValidation.ToString());
            }                

            Permiso currentPermiso = await _unitOfWork.PermisoRepository.GetByIdAsync(permisoUpdatedId);

            if (currentPermiso == null)
                throw new ArgumentException("El id de Permiso es inválido");

            var tipoPermiso = await _unitOfWork.TipoPermisoRepository.GetByIdAsync(permisoUpdated.IdTipoPermiso);

            if (tipoPermiso == null)
            {
                throw new ArgumentException("El tipo permiso enviado no es válido");
            }

            currentPermiso.NombreEmpleado = permisoUpdated.NombreEmpleado;
            currentPermiso.ApellidoEmpleado = permisoUpdated.ApellidoEmpleado;
            currentPermiso.IdTipoPermiso = permisoUpdated.IdTipoPermiso;
            currentPermiso.FechaPermiso = permisoUpdated.FechaPermiso;

            await _unitOfWork.CommitAsync();

            return await _unitOfWork.PermisoRepository.GetByIdAsync(permisoUpdatedId);
            
            //return currentPermiso;

        }
    }
}
