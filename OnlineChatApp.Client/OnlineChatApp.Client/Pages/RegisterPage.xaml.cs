namespace OnlineChatApp.Client.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}