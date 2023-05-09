using Ampulheta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Domain.Results
{
    public class LoginDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
