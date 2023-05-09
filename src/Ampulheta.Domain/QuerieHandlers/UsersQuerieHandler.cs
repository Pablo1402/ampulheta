using Ampulheta.Domain.Intefaces.Repositories;
using Ampulheta.Domain.Notification;
using Ampulheta.Domain.Queries;
using Ampulheta.Domain.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Domain.QuerieHandlers
{
    public class UsersQuerieHandler : IRequestHandler<UsersQuerie, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly NotificationContext _notificationContext;

        public UsersQuerieHandler(IUserRepository userRepository
            , NotificationContext notificationContext)
        {
            _userRepository = userRepository;
            _notificationContext = notificationContext;
        }

        public async Task<List<UserDto>> Handle(UsersQuerie request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetPaged(request.PageIndex,request.PageSize);

            if(users == null || !users.Any())
            {
                _notificationContext.AddNotification("Users", "Nenhum usuário encontrado!", (int)HttpStatusCode.NotFound);
                return null;
            }

            var data = new List<UserDto>();
            users.ForEach(x => data.Add(new UserDto() 
            { 
                Email = x.Email,
                Id = x.Id,
                Login = x.Login,
                Name = x.Name
            }));
            return data;
        }
    }
}
