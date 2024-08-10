using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Solution.Core.Entities
{
    [Table("TipoPermiso")]
    public class TipoPermiso
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        [NotMapped]
        public ICollection<Permiso> Permisos { get; } = new List<Permiso>();
    }
}
