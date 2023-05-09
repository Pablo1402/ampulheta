using Ampulheta.Domain.Commands;
using Ampulheta.Domain.Dtos;
using Ampulheta.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ampulheta.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ProjetcController : BaseController
    {
        private readonly IMediator _mediator;

        public ProjetcController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "ADMIN, USER")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ProjectDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromQuery] ProjectQuerie querie)
        {
            return Ok(await _mediator.Send(querie));
        }

        [Authorize(Roles = "ADMIN, USER")]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProjectDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById( int id)
        {
            return Ok(await _mediator.Send(new ProjectByIdQuerie() { Id = id}));
        }

        [Authorize(Roles = "ADMIN, USER")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProjectDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProjectDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateProjectCommand command, int id)
        {
            if (id != command.Id)
                return BadRequest("Invalid data!");
            return Ok(await _mediator.Send(command));
        }

    }
}
