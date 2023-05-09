using Ampulheta.Domain.Commands;
using Ampulheta.Domain.Helpers;
using Ampulheta.Domain.Intefaces.Repositories;
using Ampulheta.Domain.Intefaces.Services;
using Ampulheta.Domain.Notification;
using Ampulheta.Domain.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Domain.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly NotificationContext _notificationContext;

        public LoginCommandHandler(IAuthService authService
            , IUserRepository userRepository
            , NotificationContext notificationContext)
        {
            _authService = authService;
            _userRepository = userRepository;
            _notificationContext = notificationContext;
        }

        public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByLogin(request.Login);
            if (user == null || user.Password != Encript.Sha256(request.Password))
            {
                _notificationContext.AddNotification("Auth", "Usuario e/ou senha inválidos", null);
                return null;
            }

            var token = _authService.GenerateToken(user);

            return new LoginDto()
            {
                Token = token,
                User = new UserDto()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Login = user.Login
                }
            };

        }
    }
}
