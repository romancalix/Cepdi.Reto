using Cepdi.Application.UseCases.Commons.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepdi.Application.UseCases.UseCases.Medicamentos.Commands.DeleteCommand
{
    public class DeleteMedicamentoCommand : IRequest<BaseResponse<bool>>
    {
        public int id { get; set; }
    }
}
