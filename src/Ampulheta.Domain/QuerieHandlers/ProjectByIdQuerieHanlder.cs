using Ampulheta.Domain.Dtos;
using Ampulheta.Domain.Intefaces.Repositories;
using Ampulheta.Domain.Notification;
using Ampulheta.Domain.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Domain.QuerieHandlers
{
    public class ProjectByIdQuerieHanlder : IRequestHandler<ProjectByIdQuerie, ProjectDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly NotificationContext _notificationContext;

        public ProjectByIdQuerieHanlder(IProjectRepository projectRepository, NotificationContext notificationContext)
        {
            _projectRepository = projectRepository;
            _notificationContext = notificationContext;
        }

        public async Task<ProjectDto> Handle(ProjectByIdQuerie request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetById(request.Id);
            if (project == null)
            {
                _notificationContext.AddNotification("ProjectById", "Não existe projeto com esse identificador", (int)HttpStatusCode.NotFound);
                return null;
            }

            return new ProjectDto()
            {
                Id = project.Id,
                Name = project.Name,
                Note = project.Note,
            };
        }
    }
}
