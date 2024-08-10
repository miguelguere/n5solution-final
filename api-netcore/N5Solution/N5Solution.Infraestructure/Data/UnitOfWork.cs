using N5Solution.Core.Interfaces;
using N5Solution.Core.Interfaces.Repositories;
using N5Solution.Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Solution.Infraestructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly N5DataDBContext _context;
        private IPermisoRepository _permisoRepository;
        private ITipoPermisoRepository _tipoPermisoRepository;

        public UnitOfWork(N5DataDBContext context)
        {
            _context = context;

        }
        public IPermisoRepository PermisoRepository => _permisoRepository ??= new PermisoRepository(_context);

        public ITipoPermisoRepository TipoPermisoRepository => _tipoPermisoRepository ??= new TipoPermisoRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
