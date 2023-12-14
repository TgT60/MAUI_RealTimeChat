using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChatApp.Client.Services.Register
{
	public class RegisterResponse : BaseResponse
	{
		public int Id { get; set; }
		public string LoginId { get; set; } = null!;
		public string UserName { get; set; } = null!;
		public string Token { get; set; }

	}
}
