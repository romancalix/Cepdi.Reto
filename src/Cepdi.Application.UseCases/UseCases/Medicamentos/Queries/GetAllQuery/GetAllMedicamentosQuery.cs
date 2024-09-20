using Cepdi.Application.DTO;
using Cepdi.Application.UseCases.Commons.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepdi.Application.UseCases.UseCases.Medicamentos.Queries.GetAllQuery
{
    public class GetAllMedicamentosQuery : IRequest<BaseResponse<IEnumerable<MedicamentoDTO>>>
    {
    }
}
