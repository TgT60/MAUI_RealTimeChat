using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChatApp.Client.Services.Authenticate
{
	public class AuthenticateResponse
	{
        public int Id { get; set; }
		public string Username { get; set; }
		public string Token { get; set; }
    }
}
