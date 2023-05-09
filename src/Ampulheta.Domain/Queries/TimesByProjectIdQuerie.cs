using Ampulheta.Domain.Dtos;
using MediatR;

namespace Ampulheta.Domain.Queries
{
    public class TimesByProjectIdQuerie : IRequest<List<TimeDto>>
    {
        public int ProjectId { get; set; }
    }
}
