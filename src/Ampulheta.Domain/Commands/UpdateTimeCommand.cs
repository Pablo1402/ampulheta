using Ampulheta.Domain.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ampulheta.Domain.Commands
{
    public class UpdateTimeCommand : IRequest<TimeDto>
    {
        [Required]
        public int Id { get; set; }

        public int? UserId { get; set; }
        [Required]
        public int ProjectId { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        [Required]
        public DateTime EndedAt { get; set; }
    }
}
