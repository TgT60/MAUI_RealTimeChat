namespace OnlineChatApp.Client
{
	public partial class AppShell : Shell
	{
		public AppShell(MemberPage loginPage)
		{
			InitializeComponent();

			Routing.RegisterRoute("ListChatPage", typeof(ListChatPage));
			Routing.RegisterRoute("ChatPage", typeof(ChatPage));
			Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));

			this.CurrentItem = loginPage;
		}

	}
}
