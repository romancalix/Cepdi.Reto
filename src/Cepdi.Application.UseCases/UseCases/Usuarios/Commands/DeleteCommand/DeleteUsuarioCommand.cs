using System;
using Cepdi.Application.UseCases.Commons.Base;
using MediatR;

namespace Cepdi.Application.UseCases.UseCases.Usuarios.Commands.DeleteCommand;

public class DeleteUsuarioCommand : IRequest<BaseResponse<bool>>
{
  public int id { get; set; }
}
