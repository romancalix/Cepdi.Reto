using Cepdi.Application.DTO;
using Cepdi.Application.UseCases.Commons.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepdi.Application.UseCases.UseCases.Usuarios.Queries.GetByLogin
{
    public class GetUsuarioByLoginQuery : IRequest<BaseResponse<UsuarioDTO>>
    {
        public string usuario { get; set; }
        public string contrasena { get; set; }
    }
}
