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

namespace Cepdi.Application.UseCases.UseCases.Usuarios.Queries.GetByLogin
{
    public class GetUsuarioByLoginHandler : IRequestHandler<GetUsuarioByLoginQuery, BaseResponse<UsuarioDTO>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public GetUsuarioByLoginHandler(IUsuarioRepository repository,
            IMapper mapper)
        {
            this._usuarioRepository = repository;
            this._mapper = mapper;
        }
        public async Task<BaseResponse<UsuarioDTO>> Handle(GetUsuarioByLoginQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<UsuarioDTO>();

            try
            {

                var usuarios = await this._usuarioRepository.GetUsuarioByLoginAsync(request.usuario, request.contrasena);

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
