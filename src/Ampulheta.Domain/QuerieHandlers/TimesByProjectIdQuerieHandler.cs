using Ampulheta.Domain.Dtos;
using Ampulheta.Domain.Intefaces.Repositories;
using Ampulheta.Domain.Notification;
using Ampulheta.Domain.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Domain.QuerieHandlers
{
    public class TimesByProjectIdQuerieHandler : IRequestHandler<TimesByProjectIdQuerie, List<TimeDto>>
    {
        private readonly ITimeRepository _timeRepository;
        private readonly NotificationContext _notificationContext;

        public TimesByProjectIdQuerieHandler(ITimeRepository timeRepository, NotificationContext notificationContext)
        {
            _timeRepository = timeRepository;
            _notificationContext = notificationContext;
        }

        public async Task<List<TimeDto>> Handle(TimesByProjectIdQuerie request, CancellationToken cancellationToken)
        {
            var times = await _timeRepository.GetByProjetc(request.ProjectId);
            if (times == null)
            {
                _notificationContext.AddNotification("GetTimeByProject", "Nenhum apontamento para este projeto", 404);
                return null;
            }
            var list = new List<TimeDto>();
            times.ForEach(x => list.Add(new TimeDto()
            {
                EndedAt = x.EndedAt,
                Id = x.Id,
                ProjectId = x.ProjectId,
                StartedAt = x.StartedAt,
                UserId = x.UserId,
                UserName = x.User.Name
            }));
            return list;
        }
    }
}
