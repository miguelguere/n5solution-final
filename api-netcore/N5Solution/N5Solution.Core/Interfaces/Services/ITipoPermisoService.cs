using N5Solution.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Solution.Core.Interfaces.Services
{
    public interface ITipoPermisoService
    {

        Task<TipoPermiso> GetTipoPermisoById(int Id);
        Task<IEnumerable<TipoPermiso>> GetAll();

        Task<TipoPermiso> CreateTipoPermiso(TipoPermiso tipoPermiso);

        Task<TipoPermiso> UpdateTipoPermiso(int tipoPermisoUpdatedId, TipoPermiso tipoPermisoUpdated);
    }
}
