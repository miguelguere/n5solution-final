using FluentValidation;
using N5Solution.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Solution.Services.Validators
{
    public class PermisoValidator: AbstractValidator<Permiso>
    {
        public PermisoValidator()
        {
            RuleFor(x => x.NombreEmpleado).NotEmpty().WithMessage("Permiso debe tener Nombre Empleado");
            RuleFor(x => x.ApellidoEmpleado).NotEmpty().WithMessage("Permiso debe tener Apellido Empleado");
            RuleFor(x => x.IdTipoPermiso).NotEmpty().WithMessage("Permiso debe tener un Tipo de Permiso");
            RuleFor(x => x.FechaPermiso).NotEmpty().WithMessage("Permiso debe tener una Fecha");
        }
    }
}
