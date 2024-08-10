using Microsoft.EntityFrameworkCore.Migrations;

namespace N5Solution.Infraestructure.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO TipoPermiso(Descripcion) VALUES ('Temporal');");
            migrationBuilder.Sql("INSERT INTO TipoPermiso(Descripcion) VALUES ('Permanente');");
            migrationBuilder.Sql("INSERT INTO Permiso(NombreEmpleado, ApellidoEmpleado, IdTipoPermiso, FechaPermiso) VALUES ('Rocky', 'Balboa', 1, GETDATE());");
            migrationBuilder.Sql("INSERT INTO Permiso(NombreEmpleado, ApellidoEmpleado, IdTipoPermiso, FechaPermiso) VALUES ('Apolo', 'Creed', 2, GETDATE() + 1);");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
