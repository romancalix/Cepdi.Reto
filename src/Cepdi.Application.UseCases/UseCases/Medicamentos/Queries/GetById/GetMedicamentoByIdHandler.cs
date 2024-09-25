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

namespace Cepdi.Application.UseCases.UseCases.Medicamentos.Queries.GetById
{
    public class GetMedicamentoByIdHandler : IRequestHandler<GetMedicamentoByIdQuery, BaseResponse<MedicamentoDTO>>
    {
        private readonly IMedicamentoRepository _repository;
        private readonly IMapper _mapper;
        public GetMedicamentoByIdHandler(IMedicamentoRepository repository,
            IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public  async Task<BaseResponse<MedicamentoDTO>> Handle(GetMedicamentoByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<MedicamentoDTO>();

            try
            {

                var medicamentos = await this._repository.GetMedicamentoByIdAsync(request.id);

                if (medicamentos != null)
                {
                    response.IsSuccess = true;
                    response.Data = this._mapper.Map<MedicamentoDTO>(medicamentos);
                    response.Message = "Consulta éxitosa.";
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
