using AutoMapper;
using Cepdi.Application.Interfaces;
using Cepdi.Application.UseCases.Commons.Base;
using MediatR;
using ENTITIES = Cepdi.Domain.Entities;

namespace Cepdi.Application.UseCases.UseCases.Usuarios.Commands.CreateCommand
{
    public class CreateUsuarioHandler : IRequestHandler<CreateUsuarioCommand, BaseResponse<bool>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public CreateUsuarioHandler(
            IUsuarioRepository usuarioRepository,
            IMapper mapper)
        {
            this._usuarioRepository = usuarioRepository;
            this._mapper = mapper;
        }
        public async Task<BaseResponse<bool>> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUsuarioValidation();
            var validationResults = await validator.ValidateAsync(request, cancellationToken);
            var response = new BaseResponse<bool>();

            if (validationResults.Errors.Any())
            {
                response.IsSuccess = false;
                response.Message = "Datos incorrectos";

                response.Errors = validationResults
                    .Errors
                    .Select(x => new BaseError() { 
                        errorMessage = x.ErrorMessage, 
                        propertyName = x.PropertyName 
                    });

                return response;
            }

            try
            {
                var usuario = this._mapper.Map<ENTITIES.Usuarios>(request);
                response.Data = await this._usuarioRepository.AddUsuarioAsync(usuario);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Usuario agregado con éxito.";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
