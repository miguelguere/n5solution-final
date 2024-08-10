using N5Solution.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Solution.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPermisoRepository PermisoRepository { get; }
        ITipoPermisoRepository TipoPermisoRepository { get; }

        Task<int> CommitAsync();
    }
}
