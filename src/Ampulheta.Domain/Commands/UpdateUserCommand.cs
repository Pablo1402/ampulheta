using Ampulheta.Domain.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Domain.Commands
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(400)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Invalid email...")]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Login { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}
