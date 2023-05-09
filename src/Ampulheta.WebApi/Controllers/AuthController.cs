using Ampulheta.Domain.Commands;
using Ampulheta.Domain.Notification;
using Ampulheta.Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ampulheta.WebApi.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Auth([FromBody] LoginCommand command)
        {
            var data = await _mediator.Send(command);
            return Ok(data);
        }
    }
}
