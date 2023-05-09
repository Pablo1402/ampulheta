using Ampulheta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Domain.Intefaces.Services
{
    public interface IAuthService
    {
        public string GenerateToken(User user);
    }
}
