using Ampulheta.Domain.Commands;
using Ampulheta.Domain.Dtos;
using Ampulheta.Domain.Intefaces.Repositories;
using Ampulheta.Domain.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Ampulheta.Domain.Entities;

namespace Ampulheta.Domain.CommandHandlers
{
    public class RegisterTimeCommandHandler : IRequestHandler<RegisterTimeCommand, TimeDto>
    {
        private readonly ITimeRepository _timeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly NotificationContext _notificationContext;
        private readonly IHttpContextAccessor _accessor;

        public RegisterTimeCommandHandler(ITimeRepository timeRepository,
            NotificationContext notificationContext,
            IUserRepository userRepository,
            IProjectRepository projectRepository, IHttpContextAccessor accessor)
        {
            _timeRepository = timeRepository;
            _notificationContext = notificationContext;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _accessor = accessor;
        }

        public async Task<TimeDto> Handle(RegisterTimeCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);
            if (_notificationContext.HasNotifications)
                return null;

            var time = new Time()
            {
                EndedAt = request.EndedAt,
                ProjectId = request.ProjectId,
                StartedAt = request.StartedAt,
                UserId = _accessor.HttpContext.User.IsInRole("USER") ? Convert.ToInt32( _accessor.HttpContext.User.Identity.Name ): request.UserId.Value,
            };
            await _timeRepository.SaveAsync(time);
            return new TimeDto()
            {
                EndedAt = time.EndedAt,
                ProjectId = time.ProjectId,
                StartedAt = time.StartedAt,
                Id = time.Id,
                UserId = time.UserId
            };
        }

        private async Task ValidateRequest(RegisterTimeCommand request)
        {
            if (request.StartedAt > request.EndedAt)
                _notificationContext.AddNotification("CreateTime", "Data inválidas", (int)HttpStatusCode.BadRequest);

            var project = await _projectRepository.GetById(request.ProjectId);
            if (project == null)
                _notificationContext.AddNotification("CreateTime", "Projeto não existe", (int)HttpStatusCode.NotFound);

            if(_accessor.HttpContext.User.IsInRole("ADMIN") && request.UserId == null)
                _notificationContext.AddNotification("CreateTime", "Informe usuario", (int)HttpStatusCode.BadRequest);

            if (request.UserId != null)
            {
                var user = await _userRepository.GetById(request.UserId.Value);
                if (user == null)
                    _notificationContext.AddNotification("CreateTime", "Usuário não existe", (int)HttpStatusCode.NotFound);

                if (!_accessor.HttpContext.User.IsInRole("ADMIN")
                    && _accessor.HttpContext.User.Identity.Name != request.UserId.Value.ToString())
                    _notificationContext.AddNotification("CreateTime", "Voce não tem permissão para criar um apontamento para esse usuário"
                        , (int)HttpStatusCode.NotFound);
            }
        }
    }
}
