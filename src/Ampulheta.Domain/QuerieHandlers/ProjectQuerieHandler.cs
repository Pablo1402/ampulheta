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
    public class ProjectQuerieHandler : IRequestHandler<ProjectQuerie, List<ProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly NotificationContext _notificationContext;

        public ProjectQuerieHandler(IProjectRepository projectRepository, NotificationContext notificationContext)
        {
            _projectRepository = projectRepository;
            _notificationContext = notificationContext;
        }

        public async Task<List<ProjectDto>> Handle(ProjectQuerie request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetPaged(request.PageIndex, request.PageSize);

            if (projects == null || !projects.Any())
            {
                _notificationContext.AddNotification("Projects", "Nenhum projeto encontrado!", (int)HttpStatusCode.NotFound);
                return null;
            }

            var data = new List<ProjectDto>();
            projects.ForEach(x => data.Add(new ProjectDto()
            {
                Id = x.Id,
                Note = x.Note,
                Name = x.Name
            }));
            return data;
        }
    }
}
