using Microsoft.EntityFrameworkCore;
using N5Solution.Core.Entities;
using N5Solution.Infraestructure.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Solution.Infraestructure.Data
{
    public class N5DataDBContext : DbContext
    {
        public DbSet<TipoPermiso> TipoPermisos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public N5DataDBContext()
        {

        }
        public N5DataDBContext(DbContextOptions<N5DataDBContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PermisoConfiguration());
            modelBuilder.ApplyConfiguration(new TipoPermisoConfiguration());

        }
    }
}
