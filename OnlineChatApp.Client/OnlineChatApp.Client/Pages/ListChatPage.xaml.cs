namespace OnlineChatApp.Client.Pages;

public partial class ListChatPage : ContentPage
{
	public ListChatPage(ListChatPageViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}

	private void ListChatPage_OnNavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(this.BindingContext as ListChatPageViewModel).Initialize();
	}
}