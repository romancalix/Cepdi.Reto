using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITIES = Cepdi.Domain.Entities;
using DTO = Cepdi.Application.DTO;
using Cepdi.Application.UseCases.UseCases.Usuarios.Commands.CreateCommand;
using Cepdi.Application.UseCases.UseCases.Usuarios.Commands.UpdateCommand;
using Cepdi.Application.DTO;
using Cepdi.Application.UseCases.UseCases.Medicamentos.Commands.CreateCommand;

namespace Cepdi.Application.UseCases.Mapping
{
    public class UsuarioMappingProfile : Profile
    {
        public UsuarioMappingProfile()
        {
            CreateMap<ENTITIES.Usuarios, CreateUsuarioCommand>()
                //.ForMember(x => x.id, x => x.MapFrom(y => y.IDUSUARIO))
                .ForMember(x => x.nombre, x => x.MapFrom(y => y.NOMBRE))
                //.ForMember(x => x.status, x => x.MapFrom(y => y.ESTATUS))
                .ForMember(x => x.usuario, x => x.MapFrom(y => y.USUARIO))
                .ForMember(x => x.password, x => x.MapFrom(y => y.PASSWORD))
                .ForMember(x => x.acciones, x => x.MapFrom(y => y.ACCIONES))
                //.ForMember(x => x.creacion, x => x.MapFrom(y => y.FECHACREACION))
                .ReverseMap();

            CreateMap<ENTITIES.Usuarios, UpdateUsuarioCommand>()
               .ForMember(x => x.id, x => x.MapFrom(y => y.IDUSUARIO))
               .ForMember(x => x.nombre, x => x.MapFrom(y => y.NOMBRE))
               .ForMember(x => x.status, x => x.MapFrom(y => y.ESTATUS))
               .ForMember(x => x.usuario, x => x.MapFrom(y => y.USUARIO))
               .ForMember(x => x.password, x => x.MapFrom(y => y.PASSWORD))
               .ForMember(x => x.acciones, x => x.MapFrom(y => y.ACCIONES))
               .ReverseMap();


            CreateMap<ENTITIES.Usuarios, UsuarioDTO>()
               .ForMember(x => x.id, x => x.MapFrom(y => y.IDUSUARIO))
               .ForMember(x => x.nombre, x => x.MapFrom(y => y.NOMBRE))
               .ForMember(x => x.status, x => x.MapFrom(y => y.ESTATUS))
               .ForMember(x => x.usuario, x => x.MapFrom(y => y.USUARIO))
               .ForMember(x => x.password, x => x.MapFrom(y => y.PASSWORD))
               .ForMember(x => x.acciones, x => x.MapFrom(y => y.ACCIONES))
               .ReverseMap();
        }
    }
}
