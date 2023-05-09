using Ampulheta.Domain.Commands;
using Ampulheta.Domain.Dtos;
using Ampulheta.Domain.Entities;
using Ampulheta.Domain.Intefaces.Repositories;
using Ampulheta.Domain.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Domain.CommandHandlers
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly NotificationContext _notificationContext;

        public CreateProjectCommandHandler(IProjectRepository projectRepository, NotificationContext notificationContext)
        {
            _projectRepository = projectRepository;
            _notificationContext = notificationContext;
        }

        public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project()
            {
                Name = request.Name,
                Note = request.Note
            };
            await _projectRepository.SaveAsync(project);
            return new ProjectDto()
            {
                Id = project.Id,
                Note = request.Note,
                Name = request.Name
            };
        }
    }
}
