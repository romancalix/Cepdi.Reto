using AutoMapper;
using Cepdi.Application.UseCases.UseCases.Usuarios.Commands.CreateCommand;
using ENTITIES = Cepdi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cepdi.Application.UseCases.UseCases.Medicamentos.Commands.CreateCommand;
using Cepdi.Application.DTO;
using Cepdi.Application.UseCases.UseCases.Medicamentos.Commands.UpdateCommand;

namespace Cepdi.Application.UseCases.Mapping
{
    public class MedicamentoMappingProfile : Profile
    {
        public MedicamentoMappingProfile()
        {
            CreateMap<ENTITIES.Medicamentos, CreateMedicamentoCommand>()
               .ReverseMap();

            CreateMap<ENTITIES.Medicamentos, MedicamentoDTO>()
               .ReverseMap();

            CreateMap<ENTITIES.Medicamentos, UpdateMedicamentoCommand>()
             .ReverseMap();
        }
    }
}
