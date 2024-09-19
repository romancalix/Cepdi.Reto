using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepdi.Application.UseCases.UseCases.Medicamentos.Commands.CreateCommand
{
    public class CreateMedicamentoValidation : AbstractValidator<CreateMedicamentoCommand>
    {
        public CreateMedicamentoValidation()
        {
            RuleFor(x => x.NOMBRE)
              .NotNull().WithMessage("El campo NOMBRE es requerdo.")
              .NotEmpty().WithMessage("El campo NOMBRE no debe estar vacío.");

            RuleFor(x => x.PRECIO)
               .NotNull().WithMessage("El campo PRECIO es requerdo.")
               .NotEmpty().WithMessage("El campo PRECIO no debe estar vacío.")
               .GreaterThan(0).WithMessage("El PRECIO debe ser mayor a 0 (cero)") ;

            RuleFor(x => x.PRESENTACION)
              .NotNull().WithMessage("El campo PRESENTACION es requerdo.")
              .NotEmpty().WithMessage("El campo PRESENTACION no debe estar vacío.");

        }
    }
}
