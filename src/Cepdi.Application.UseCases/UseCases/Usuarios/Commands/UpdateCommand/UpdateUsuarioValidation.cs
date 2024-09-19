using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepdi.Application.UseCases.UseCases.Usuarios.Commands.UpdateCommand
{
    public class UpdateUsuarioValidation : AbstractValidator<UpdateUsuarioCommand>
    {
        public UpdateUsuarioValidation()
        {
            RuleFor(x => x.usuario)
               .NotNull().WithMessage("El campo usuario es requerdo.")
               .NotEmpty().WithMessage("El campo usuario no debe estar vacío.");

            RuleFor(x => x.password)
               .NotNull().WithMessage("El campo password es requerdo.")
               .NotEmpty().WithMessage("El campo password no debe estar vacío.");

            RuleFor(x => x.nombre)
              .NotNull().WithMessage("El campo nombre es requerdo.")
              .NotEmpty().WithMessage("El campo nombre no debe estar vacío.");

            RuleFor(x => x.id)
            .NotNull().WithMessage("El campo id es requerdo.")
            .GreaterThanOrEqualTo(1)
            .NotEmpty().WithMessage("El campo id no debe estar vacío.");

            RuleFor(x => x.status)
           .NotNull().WithMessage("El status status es requerdo.")
           .NotEmpty().WithMessage("El status status no debe estar vacío.");
        }
    }
}
