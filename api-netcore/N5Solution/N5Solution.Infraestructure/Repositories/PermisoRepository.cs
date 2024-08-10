using N5Solution.Core.Entities;
using N5Solution.Core.Interfaces.Repositories;
using N5Solution.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Solution.Infraestructure.Repositories
{
    public class PermisoRepository : BaseRepository<Permiso>, IPermisoRepository
    {
        public PermisoRepository(N5DataDBContext context): base(context)
        {

        }
    }
}
