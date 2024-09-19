using Cepdi.Application.UseCases.Commons.Base;
using MediatR;

namespace Cepdi.Application.UseCases.UseCases.Medicamentos.Commands.CreateCommand
{
    public class CreateMedicamentoCommand : IRequest<BaseResponse<bool>>
    {
        //public int IDMEDICAMENTO { get; set; }
        public string? CONCENTRACION { get; set; }
        public string? NOMBRE { get; set; }
        public int IDFORMAFARMACEUTICA { get; set; }
        public double PRECIO { get; set; }
        public string? PRESENTACION { get; set; }
        //public bool HABILITADO { get; set; }
        public string? ACCIONES { get; set; }
    }
}
