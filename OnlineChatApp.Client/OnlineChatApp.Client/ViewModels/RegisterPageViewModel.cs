namespace OnlineChatApp.Client.ViewModels
{
	public class RegisterPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private ServiceProvider _serviceProvider;

		public RegisterPageViewModel(ServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		private string userName;
		private string loginId;
		private string password;

	}
}
