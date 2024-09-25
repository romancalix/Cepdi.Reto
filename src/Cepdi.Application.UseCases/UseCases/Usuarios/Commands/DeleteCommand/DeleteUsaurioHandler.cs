using System;
using AutoMapper;
using Cepdi.Application.Interfaces;
using Cepdi.Application.UseCases.Commons.Base;
using MediatR;

namespace Cepdi.Application.UseCases.UseCases.Usuarios.Commands.DeleteCommand;

public class DeleteUsaurioHandler : IRequestHandler<DeleteUsuarioCommand, BaseResponse<bool>>
{

  private readonly IUsuarioRepository _usuarioRepository;
  private readonly IMapper _mapper;
  public DeleteUsaurioHandler(
    IUsuarioRepository usuarioRepository,
    IMapper mapper
  )
  {
    this._usuarioRepository = usuarioRepository;
    this._mapper = mapper;
  }
  public async Task<BaseResponse<bool>> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
  {
    var response = new BaseResponse<bool>();

    try
    {

            if (await this._usuarioRepository.DeleteUsuarioAsync(request.id))
            {
                //response.Data = await this._usuarioRepository.DeleteUsuarioAsync(request.id);
                response.IsSuccess = true;
                response.Message = "Elemnto eliminado con éxito";
            }
            else 
            {
                response.IsSuccess = false;
                response.Message = "No se pudo eliminar el registro";

            }
     
    }
    catch (System.Exception e)
    {
      response.IsSuccess = false;
      response.Message = e.Message;
    }

    return response;
  }
}
