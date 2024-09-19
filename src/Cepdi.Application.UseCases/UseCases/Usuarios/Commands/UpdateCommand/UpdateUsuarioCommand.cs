using Cepdi.Application.UseCases.Commons.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepdi.Application.UseCases.UseCases.Usuarios.Commands.UpdateCommand
{
    public class UpdateUsuarioCommand : IRequest<BaseResponse<bool>>
    {
        public int id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string usuario { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public bool status { get; set; }
        public string? acciones { get; set; }
    }
}
