using N5Solution.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Solution.Core.Interfaces.Services
{
    public interface IPermisoService
    {
        Task<Permiso> GetPermisoById(int Id);
        Task<IEnumerable<Permiso>> GetAll();

        Task<Permiso> CreatePermiso(Permiso permiso);

        Task<Permiso> UpdatePermiso(int permisoUpdatedId, Permiso permisoUpdated);

    }
}
