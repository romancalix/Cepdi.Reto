using FluentValidation;

namespace Cepdi.Application.UseCases.UseCases.Usuarios.Commands.CreateCommand
{
    public class CreateUsuarioValidation : AbstractValidator<CreateUsuarioCommand>
    {
        public CreateUsuarioValidation()
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

        }
    }
}
