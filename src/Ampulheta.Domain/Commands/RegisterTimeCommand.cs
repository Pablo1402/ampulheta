using Ampulheta.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Domain.Commands
{
    public class RegisterTimeCommand : IRequest<TimeDto>
    {
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
