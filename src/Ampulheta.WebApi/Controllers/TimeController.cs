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
    public class TimeController : BaseController
    {
        private readonly IMediator _mediator;

        public TimeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "ADMIN, USER")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TimeDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateTime([FromBody] RegisterTimeCommand command)
        {
            var time = await _mediator.Send(command);
            return Ok(time);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        [Route("{projectId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ProjectDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByProjectId(int projectId)
        {
            return Ok(await _mediator.Send(new TimesByProjectIdQuerie() { ProjectId = projectId }));
        }

        [Authorize(Roles = "USER,ADMIN")]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TimeDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateTimeCommand command, int id)
        {
            if (id != command.Id)
                return BadRequest("Invalid data!");
            return Ok(await _mediator.Send(command));
        }
    }
}
