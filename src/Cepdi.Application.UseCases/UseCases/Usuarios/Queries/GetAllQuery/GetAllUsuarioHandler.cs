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

namespace Cepdi.Application.UseCases.UseCases.Usuarios.Queries.GetAllQuery
{
    public class GetAllUsuarioHandler : IRequestHandler<GetAllUsuarioQuery, BaseResponse<IEnumerable<UsuarioDTO>>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public GetAllUsuarioHandler(
            IUsuarioRepository usuarioRepository,
            IMapper mapper
            )
        {
            this._usuarioRepository = usuarioRepository;
            this._mapper = mapper;
        }
        public async Task<BaseResponse<IEnumerable<UsuarioDTO>>> Handle(GetAllUsuarioQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<UsuarioDTO>>();

            try
            {

                var usuarios = await this._usuarioRepository.GetUsuariosAsync();

                if (usuarios != null)
                {
                    response.IsSuccess = true;
                    response.Data = this._mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
                    response.Message = $"Total de registros encontrados: {usuarios.Count()}";
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
