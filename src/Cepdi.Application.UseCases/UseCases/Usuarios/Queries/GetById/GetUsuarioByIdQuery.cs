using Cepdi.Application.DTO;
using Cepdi.Application.UseCases.Commons.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepdi.Application.UseCases.UseCases.Usuarios.Queries.GetById
{
    public class GetUsuarioByIdQuery : IRequest<BaseResponse<UsuarioDTO>>
    {
        public int id { get; set; }
    }
}
