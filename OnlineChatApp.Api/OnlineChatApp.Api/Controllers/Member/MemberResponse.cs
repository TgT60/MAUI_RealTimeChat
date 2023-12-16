namespace OnlineChatApp.Api.Controllers.Member
{
	public class MemberResponse
	{
		public User User { get; set; } = null!;
		public IEnumerable<User> AllMembers { get; set; } = null!;
	}
}
