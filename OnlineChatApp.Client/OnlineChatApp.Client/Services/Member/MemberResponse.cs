using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChatApp.Client.Services.Member
{
	public class MemberResponse : BaseResponse
	{
		public User User { get; set; }
		public IEnumerable<User> AllMembers { get; set; }
	}
}
