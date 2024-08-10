using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5Solution.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Solution.Infraestructure.Data.Configurations
{
    public class PermisoConfiguration : IEntityTypeConfiguration<Permiso>
    {
        public void Configure(EntityTypeBuilder<Permiso> builder)
        {
            builder.Property(x => x.NombreEmpleado).HasColumnType("VARCHAR").HasMaxLength(4000).IsRequired();
            builder.Property(x => x.ApellidoEmpleado).HasColumnType("VARCHAR").HasMaxLength(4000).IsRequired();
            builder.Property(x => x.FechaPermiso).HasColumnType("DATETIME").IsRequired();
            builder.HasOne(x => x.TipoPermiso).WithMany(x => x.Permisos).HasForeignKey(x => x.IdTipoPermiso).IsRequired();

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

        }
    }
}
