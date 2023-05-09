using Ampulheta.Domain.Notification;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace Ampulheta.WebApi.Filter
{
	public class NotificationFilter : IAsyncResultFilter
	{
		private readonly NotificationContext _notificationContext;

		public NotificationFilter(NotificationContext notificationContext)
		{
			_notificationContext = notificationContext;
		}

		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			if (_notificationContext.HasNotifications)
			{
				context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				if(_notificationContext.Notifications.Count == 1 
					&& _notificationContext.Notifications.First().StatusCode != null)
					context.HttpContext.Response.StatusCode = _notificationContext.Notifications.First().StatusCode.Value;

				context.HttpContext.Response.ContentType = "application/json";

				var notifications = JsonConvert.SerializeObject(_notificationContext.Notifications);
				await context.HttpContext.Response.WriteAsync(notifications);

				return;
			}

			await next();
		}
	}
}
