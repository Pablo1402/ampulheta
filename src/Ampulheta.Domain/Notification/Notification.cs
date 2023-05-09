using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ampulheta.Domain.Notification
{
	public class Notification
	{
		public string Key { get; }
		public string Message { get; }
        public int? StatusCode { get;  }

		public Notification(string key, string message, int? statusCode = null)
		{
			Key = key;
			Message = message;
			StatusCode = statusCode;
		}
	}
}
