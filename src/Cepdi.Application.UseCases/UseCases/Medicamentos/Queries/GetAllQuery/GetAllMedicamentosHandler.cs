using AutoMapper;
using Cepdi.Application.DTO;
using Cepdi.Application.Interfaces;
using Cepdi.Application.UseCases.Commons.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepdi.Application.UseCases.UseCases.Medicamentos.Queries.GetAllQuery
{
    internal class GetAllMedicamentosHandler : IRequestHandler<GetAllMedicamentosQuery, BaseResponse<IEnumerable<MedicamentoDTO>>>
    {
        private readonly IMedicamentoRepository _repository;
        private readonly IMapper _mapper;
        public GetAllMedicamentosHandler(
             IMedicamentoRepository repository,
            IMapper mapper
            )
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<BaseResponse<IEnumerable<MedicamentoDTO>>> Handle(GetAllMedicamentosQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<MedicamentoDTO>>();

            try
            {

                var medicamentos = await this._repository.GetMedicamentosAsync();

                if (medicamentos != null)
                {
                    response.IsSuccess = true;
                    response.Data = this._mapper.Map<IEnumerable<MedicamentoDTO>>(medicamentos);
                    response.Message = $"Total de registros encontrados: {medicamentos.Count()}";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "NO se encontraron registros";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
