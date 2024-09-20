using Cepdi.Application.UseCases.UseCases.Medicamentos.Commands.CreateCommand;
using Cepdi.Application.UseCases.UseCases.Medicamentos.Commands.UpdateCommand;
using Cepdi.Application.UseCases.UseCases.Usuarios.Commands.CreateCommand;
using Cepdi.Application.UseCases.UseCases.Usuarios.Commands.DeleteCommand;
using Cepdi.Application.UseCases.UseCases.Usuarios.Commands.UpdateCommand;
using Cepdi.Application.UseCases.UseCases.Usuarios.Queries.GetAllQuery;
using Cepdi.Application.UseCases.UseCases.Usuarios.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cepdi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MedicamentoController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> AgregarMedidamento([FromBody] CreateMedicamentoCommand createMedicamentoCommand)
        {
            var response = await this._mediator.Send(createMedicamentoCommand);
            return Ok(response);
        }


        [HttpPut("actualizar")]
        public async Task<IActionResult> ActualizarMedicamento([FromBody] UpdateMedicamentoCommand updateMedicamentoCommand)
        {
            var response = await this._mediator.Send(updateMedicamentoCommand);
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

    }
}
