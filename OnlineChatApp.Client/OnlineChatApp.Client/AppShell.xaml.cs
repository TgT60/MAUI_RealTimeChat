namespace OnlineChatApp.Client
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute("ListChatPage", typeof(ListChatPage));
		}
	}
}
