namespace OnlineChatApp.Api.Functions.Message
{
	public interface IMessageFunction
	{
		Task<IEnumerable<LastestMessage>> GetLastestMessage(int userId);
	}
}
