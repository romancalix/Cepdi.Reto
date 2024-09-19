using AutoMapper;
using Cepdi.Application.UseCases.UseCases.Usuarios.Commands.CreateCommand;
using ENTITIES = Cepdi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cepdi.Application.UseCases.UseCases.Medicamentos.Commands.CreateCommand;

namespace Cepdi.Application.UseCases.Mapping
{
    public class MedicamentoMappingProfile : Profile
    {
        public MedicamentoMappingProfile()
        {
            CreateMap<ENTITIES.Medicamentos, CreateMedicamentoCommand>()
               .ReverseMap();
        }
    }
}
