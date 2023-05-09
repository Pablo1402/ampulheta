using Ampulheta.Domain.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ampulheta.Domain.Commands
{
    public class UpdateProjectCommand : IRequest<ProjectDto>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(400)]
        public string Note { get; set; }
    }
}
