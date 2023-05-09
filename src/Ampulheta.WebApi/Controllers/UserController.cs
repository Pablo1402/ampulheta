using Ampulheta.Domain.Commands;
using Ampulheta.Domain.Queries;
using Ampulheta.Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ampulheta.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<UserDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromQuery] UsersQuerie querie)
        {
            return Ok(await _mediator.Send(querie));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command, [FromQuery] int id)
        {
            if (id != command.Id)
                return BadRequest("Invalid data!");
            return Ok(await _mediator.Send(command));
        }
    }
}
