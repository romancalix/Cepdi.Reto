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

namespace Cepdi.Application.UseCases.UseCases.Usuarios.Queries.GetById
{
    public class GetUsuarioByIdHandler : IRequestHandler<GetUsuarioByIdQuery, BaseResponse<UsuarioDTO>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public GetUsuarioByIdHandler(
            IUsuarioRepository usuarioRepository,
            IMapper mapper
        )
        {
            this._usuarioRepository = usuarioRepository;
            this._mapper = mapper;
        }
        public async Task<BaseResponse<UsuarioDTO>> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<UsuarioDTO>();

            try
            {

                var usuarios = await this._usuarioRepository.GetUsuariosByIdAsync(request.id);

                if (usuarios != null)
                {
                    response.IsSuccess = true;
                    response.Data = this._mapper.Map<UsuarioDTO>(usuarios);
                    response.Message = $"Consulta realizada con éxito";
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

