using Ampulheta.Domain.Commands;
using Ampulheta.Domain.Dtos;
using Ampulheta.Domain.Intefaces.Repositories;
using Ampulheta.Domain.Notification;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Domain.CommandHandlers
{
    public class UpdateTimeCommandHandler : IRequestHandler<UpdateTimeCommand, TimeDto>
    {
        private readonly ITimeRepository _timeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly NotificationContext _notificationContext;
        private readonly IHttpContextAccessor _accessor;

        public UpdateTimeCommandHandler(ITimeRepository timeRepository,
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
        public async Task<TimeDto> Handle(UpdateTimeCommand request, CancellationToken cancellationToken)
        {
            var time = await _timeRepository.GetById(request.Id);
            if (time == null)
                _notificationContext.AddNotification("UpdateTime", "Apontamento não encontrado", (int)HttpStatusCode.NotFound);

            await ValidateRequest(request);
            if (_notificationContext.HasNotifications)
                return null;

            time.ProjectId  = request.ProjectId;
            time.UserId = _accessor.HttpContext.User.IsInRole("USER") ? Convert.ToInt32(_accessor.HttpContext.User.Identity.Name) : request.UserId.Value;
            time.EndedAt = request.EndedAt;
            time.StartedAt = request.StartedAt;

            await _timeRepository.UpdateAsync(time);
            return new TimeDto()
            {
                EndedAt = time.EndedAt,
                ProjectId = time.ProjectId,
                StartedAt = time.StartedAt,
                Id = time.Id,
                UserId = time.UserId
            };
        }

        private async Task ValidateRequest(UpdateTimeCommand request)
        {
            if (request.StartedAt > request.EndedAt)
                _notificationContext.AddNotification("UpdateTime", "Data inválidas", (int)HttpStatusCode.BadRequest);

            var project = await _projectRepository.GetById(request.ProjectId);
            if (project == null)
                _notificationContext.AddNotification("UpdateTime", "Projeto não existe", (int)HttpStatusCode.NotFound);

            if (_accessor.HttpContext.User.IsInRole("ADMIN") && request.UserId == null)
                _notificationContext.AddNotification("UpdateTime", "Informe usuario", (int)HttpStatusCode.BadRequest);

            if (request.UserId != null)
            {
                var user = await _userRepository.GetById(request.UserId.Value);
                if (user == null)
                    _notificationContext.AddNotification("UpdateTime", "Usuário não existe", (int)HttpStatusCode.NotFound);

                if (!_accessor.HttpContext.User.IsInRole("ADMIN")
                    && _accessor.HttpContext.User.Identity.Name != request.UserId.Value.ToString())
                    _notificationContext.AddNotification("UpdateTime", "Voce não tem permissão para atualizar um apontamento para esse usuário"
                        , (int)HttpStatusCode.NotFound);
            }
        }
    }
}
