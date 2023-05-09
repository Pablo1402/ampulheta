using Ampulheta.Domain.Dtos;
using MediatR;

namespace Ampulheta.Domain.Queries
{
    public class ProjectQuerie : IRequest<List<ProjectDto>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
