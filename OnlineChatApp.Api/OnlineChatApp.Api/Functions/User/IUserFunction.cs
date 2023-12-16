namespace OnlineChatApp.Api.Functions.User
{
	public interface IUserFunction
	{
		User? Authenticate(string loginId, string password);
		User GetUserById(int id);
		TblUser Register(string userName,string loginId,string password);
		Task<IEnumerable<User>> GetMembers();
	}
}
