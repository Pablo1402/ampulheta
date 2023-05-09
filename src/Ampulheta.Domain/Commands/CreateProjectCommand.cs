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
    public class CreateProjectCommand : IRequest<ProjectDto>
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(400)]
        public string Note { get; set; }
    }
}
