using AutoMapper;
using Cepdi.Application.Interfaces;
using Cepdi.Application.UseCases.Commons.Base;
using MediatR;
using ENTITIES = Cepdi.Domain.Entities;

namespace Cepdi.Application.UseCases.UseCases.Medicamentos.Commands.CreateCommand
{
    public class CreateMedicamentoHandler : IRequestHandler<CreateMedicamentoCommand, BaseResponse<bool>>
    {
        private readonly IMedicamentoRepository _medicamentoRepository;
        private readonly IMapper _mapper;

        public CreateMedicamentoHandler(
             IMedicamentoRepository repository,
            IMapper mapper
        )
        {
            this._medicamentoRepository = repository;
            this._mapper = mapper;
        }
        public async Task<BaseResponse<bool>> Handle(CreateMedicamentoCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateMedicamentoValidation();
            var validationResults = await validator.ValidateAsync(request, cancellationToken);
            var response = new BaseResponse<bool>();

            if (validationResults.Errors.Any())
            {
                response.IsSuccess = false;
                response.Message = "Datos incorrectos";

                response.Errors = validationResults
                    .Errors
                    .Select(x => new BaseError()
                    {
                        errorMessage = x.ErrorMessage,
                        propertyName = x.PropertyName
                    });

                return response;
            }

            try
            {
                var medicamento = this._mapper.Map<ENTITIES.Medicamentos>(request);
                response.Data = await this._medicamentoRepository.AddMedicamentoAsync(medicamento);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Medicamento agregado con éxito.";
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
