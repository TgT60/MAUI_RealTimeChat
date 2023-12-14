using OnlineChatApp.Client.Services.Register;

namespace OnlineChatApp.Client.ViewModels
{
	public class RegisterPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private ServiceProvider _serviceProvider;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public RegisterPageViewModel(ServiceProvider serviceProvider)
		{
			UserName = "Kek6";
			LoginId = "Kek6";
			Password = "6";
			IsProcessing = false;

			RegisterCommand = new Command(() =>
			{
				if(isProcessing) return;

				if(UserName.Trim() == "" || Password.Trim() == "") return;

				isProcessing = true;

				Register().GetAwaiter().OnCompleted(() =>
				{
					isProcessing = false;
				});
			});
			this._serviceProvider = serviceProvider;
		}


		async Task Register()
		{
			try
			{
				var requset = new RegisterRequest
				{
					UserName = UserName,
					LoginId = LoginId,
					Password = Password,
				};

				var response = await _serviceProvider.Register(requset);
				if (response.StatusCode == 200)
				{
					await Shell.Current.GoToAsync($"ListChatPage?userId={response.Id}");
				}
				else
				{
					await AppShell.Current.DisplayAlert("ChatApp", response.StatusMessage, "OK");
				}
			}
			catch (Exception ex)
			{
				await AppShell.Current.DisplayAlert("ChatApp", ex.Message, "OK");
			}
		}

		private string userName;
		private string loginId;
		private string password;
		private bool isProcessing;

		public string UserName
		{
			get => userName;
			set { userName = value; OnPropertyChanged();}
		}

		public string LoginId
		{
			get => loginId;
			set { loginId = value; OnPropertyChanged(); }
		}

		public string Password
		{
			get => password;
			set { password = value; OnPropertyChanged(); }
		}

		public bool IsProcessing
		{
			get => isProcessing;
			set { isProcessing = value; OnPropertyChanged();}
		}

		public ICommand RegisterCommand { get; set; }
	}
}
