using N5Solution.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N5Solution.WebAPI.Models
{
    public class PermisoModel
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public int IdTipoPermiso { get; set; }

        //public virtual TipoPermiso TipoPermiso { get; set; }
        public DateTime FechaPermiso { get; set; }
    }
}
