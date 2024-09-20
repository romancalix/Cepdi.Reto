using AutoMapper;
using Cepdi.Application.Interfaces;
using Cepdi.Application.UseCases.Commons.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepdi.Application.UseCases.UseCases.Medicamentos.Commands.DeleteCommand
{
    public class DeleteMedicamentoHandler : IRequestHandler<DeleteMedicamentoCommand, BaseResponse<bool>>
    {
        private readonly IMedicamentoRepository _repository;
        private readonly IMapper _mapper;
        public DeleteMedicamentoHandler(IMedicamentoRepository repository,
    IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteMedicamentoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {

                if (await this._repository.DeleteMedicamnetoAsync(request.id))
                {
                    //response.Data = await this._usuarioRepository.DeleteUsuarioAsync(request.id);
                    response.IsSuccess = true;
                    response.Message = "Elemnto eliminado con éxito";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Elemento no encontrado";

                }

            }
            catch (System.Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
