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

namespace Ampulheta.Domain.CommandHandlers
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly NotificationContext _notificationContext;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository, NotificationContext notificationContext)
        {
            _projectRepository = projectRepository;
            _notificationContext = notificationContext;
        }
        public async Task<ProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetById(request.Id);
            if (project == null)
            {
                _notificationContext.AddNotification("ProjectById", "Não existe projeto com esse identificador", (int)HttpStatusCode.NotFound);
                return null;
            }

            project.Note = request.Note;
            project.Name = request.Name;

            await _projectRepository.UpdateAsync(project);

            return new ProjectDto()
            {
                Id = project.Id,
                Name = project.Name,
                Note = project.Note,
            };
        }
    }
}
