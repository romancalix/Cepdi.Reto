using Cepdi.Application.UseCases.Commons.Base;
using MediatR;

namespace Cepdi.Application.UseCases.UseCases.Usuarios.Commands.CreateCommand
{
    public class CreateUsuarioCommand : IRequest<BaseResponse<bool>>
    {
        //public int id { get; set; }
        public string nombre { get; set; } = string.Empty;
        //public DateTime creacion { get; set; }
        public string usuario { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        //public bool status { get; set; }
        public string? acciones { get; set; }
    }
}
