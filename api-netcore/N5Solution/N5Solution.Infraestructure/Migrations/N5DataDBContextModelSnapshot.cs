﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using N5Solution.Infraestructure.Data;

namespace N5Solution.Infraestructure.Migrations
{
    [DbContext(typeof(N5DataDBContext))]
    partial class N5DataDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("N5Solution.Core.Entities.Permiso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApellidoEmpleado")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("VARCHAR(4000)");

                    b.Property<DateTime>("FechaPermiso")
                        .HasColumnType("DATETIME");

                    b.Property<int>("IdTipoPermiso")
                        .HasColumnType("int");

                    b.Property<string>("NombreEmpleado")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("VARCHAR(4000)");

                    b.HasKey("Id");

                    b.HasIndex("IdTipoPermiso");

                    b.ToTable("Permiso");
                });

            modelBuilder.Entity("N5Solution.Core.Entities.TipoPermiso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("VARCHAR(4000)");

                    b.HasKey("Id");

                    b.ToTable("TipoPermiso");
                });

            modelBuilder.Entity("N5Solution.Core.Entities.Permiso", b =>
                {
                    b.HasOne("N5Solution.Core.Entities.TipoPermiso", "TipoPermiso")
                        .WithMany("Permisos")
                        .HasForeignKey("IdTipoPermiso")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoPermiso");
                });

            modelBuilder.Entity("N5Solution.Core.Entities.TipoPermiso", b =>
                {
                    b.Navigation("Permisos");
                });
#pragma warning restore 612, 618
        }
    }
}
