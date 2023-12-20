namespace OnlineChatApp.Client
{
	public partial class AppShell : Shell
	{
		public AppShell(LoginPage loginPage)
		{
			InitializeComponent();

			Routing.RegisterRoute("ListChatPage", typeof(ListChatPage));
			Routing.RegisterRoute("ChatPage", typeof(ChatPage));
			Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
			Routing.RegisterRoute("LoginPage", typeof(LoginPage));
			Routing.RegisterRoute("MemberPage", typeof(MemberPage));


			this.CurrentItem = loginPage;
		}

	}
}
