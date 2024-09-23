using Cepdi.Application.UseCases.UseCases.Usuarios.Commands.CreateCommand;
using Cepdi.Application.UseCases.UseCases.Usuarios.Commands.DeleteCommand;
using Cepdi.Application.UseCases.UseCases.Usuarios.Commands.UpdateCommand;
using Cepdi.Application.UseCases.UseCases.Usuarios.Queries.GetAllQuery;
using Cepdi.Application.UseCases.UseCases.Usuarios.Queries.GetById;
using Cepdi.Application.UseCases.UseCases.Usuarios.Queries.GetByLogin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cepdi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator)  
        {
            this._mediator = mediator;
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> AgregarUsuario([FromBody] CreateUsuarioCommand createUsuarioCommand)
        {
            var response = await this._mediator.Send(createUsuarioCommand);
            return Ok(response);
        }


        [HttpPut("actualiazr")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] UpdateUsuarioCommand updateUsuarioCommand)
        {
            var response = await this._mediator.Send(updateUsuarioCommand);
            return Ok(response);
        }

        [HttpGet("todos")]
        public async Task<IActionResult> TodoUsuarios()
        {
            var response = await this._mediator.Send(new GetAllUsuarioQuery());
            return Ok(response);
        }


        [HttpGet("porId")]
        public async Task<IActionResult> UsuarioPorId(int id)
        {
            var response = await this._mediator.Send(new GetUsuarioByIdQuery() { id = id });
            return Ok(response);
        }

        [HttpDelete("eliminar")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var response = await this._mediator.Send(new DeleteUsuarioCommand() { id = id });
            return Ok(response);
        }

        [HttpGet("Login")]
        public async Task<IActionResult> LoginUsuario(string usuario, string contrasena)
        {
            var response = await this._mediator.Send(new GetUsuarioByLoginQuery { usuario = usuario, contrasena = contrasena  });
            return Ok(response);
        }
    }
}
