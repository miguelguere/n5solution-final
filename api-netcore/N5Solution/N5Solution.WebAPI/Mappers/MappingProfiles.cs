using N5Solution.Core.Entities;
using N5Solution.WebAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N5Solution.WebAPI.Mappers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            //Entities To Model

            CreateMap<Permiso, PermisoModel>().ReverseMap();
            CreateMap<TipoPermiso, TipoPermisoModel>().ReverseMap();

        }
    }
}
