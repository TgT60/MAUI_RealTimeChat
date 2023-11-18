namespace OnlineChatApp.Api.Functions.UserFrined
{
	public interface IUserFriendFunction
	{
		Task<IEnumerable<User.User>> GetListUserFriend(int userId);
	}
}
