using Ampulheta.Domain.Commands;
using Ampulheta.Domain.Entities;
using Ampulheta.Domain.Enuns;
using Ampulheta.Domain.Helpers;
using Ampulheta.Domain.Intefaces.Repositories;
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
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly NotificationContext _notificationContext;

        public CreateUserCommandHandler(IUserRepository userRepository, NotificationContext notificationContext)
        {
            _userRepository = userRepository;
            _notificationContext = notificationContext;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userLogin = await _userRepository.GetByLogin(request.Login);
            if (userLogin != null)
            {
                _notificationContext.AddNotification("User", "Já existe um usuário com esse login");
                return null;
            }
            var user = new User()
            {
                Email = request.Email,
                Login = request.Login,
                Name = request.Name,
                Password = Encript.Sha256(request.Password),
                UserTypeId = (int)UserTypeEnum.USER
            };
            await _userRepository.SaveAsync(user);

            return new UserDto()
            {
                Email = user.Email,
                Name = user.Name,
                Id = user.Id,
                Login = user.Login
            };
        }
    }
}
