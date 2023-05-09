using Ampulheta.Domain.Commands;
using Ampulheta.Domain.Helpers;
using Ampulheta.Domain.Intefaces.Repositories;
using Ampulheta.Domain.Notification;
using Ampulheta.Domain.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Domain.CommandHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly NotificationContext _notificationContext;

        public UpdateUserCommandHandler(IUserRepository userRepository, NotificationContext notificationContext)
        {
            _userRepository = userRepository;
            _notificationContext = notificationContext;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.Id);
            if(user ==  null)
            {
                _notificationContext.AddNotification("UpdateUser", "Usuário não encontrado", (int)HttpStatusCode.NotFound);
                return null;
            }

            var userLogin = await _userRepository.GetByLogin(request.Login);
            if (userLogin != null && user.Login != userLogin.Login)
            {
                _notificationContext.AddNotification("User", "Já existe um usuário com esse login");
                return null;
            }

            user.Email = request.Email;
            user.Password = Encript.Sha256( request.Password);
            user.Login = request.Login;
            user.Name = request.Name;
             await  _userRepository.UpdateAsync(user);

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
