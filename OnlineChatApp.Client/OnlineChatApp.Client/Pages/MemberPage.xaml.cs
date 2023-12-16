namespace OnlineChatApp.Client.Pages;

public partial class MemberPage : ContentPage
{
	public MemberPage(MemberPageViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}

	private void MemberPage_OnNavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(this.BindingContext as MemberPageViewModel).Initialize();
	}
}