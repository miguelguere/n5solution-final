using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Solution.Core.Entities
{
    [Table("Permiso")]
    public class Permiso
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public int IdTipoPermiso { get; set; }

        [NotMapped]
        public virtual TipoPermiso TipoPermiso { get; set; }
        public DateTime FechaPermiso { get; set; }
    }
}
