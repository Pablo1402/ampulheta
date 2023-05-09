using Ampulheta.Domain.Dtos;
using MediatR;

namespace Ampulheta.Domain.Queries
{
    public class ProjectByIdQuerie : IRequest<ProjectDto>
    {
        public int Id { get; set; } 
    }
}
