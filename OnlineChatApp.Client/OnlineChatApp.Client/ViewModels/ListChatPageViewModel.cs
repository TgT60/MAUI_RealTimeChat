

namespace OnlineChatApp.Client.ViewModels
{
	public class ListChatPageViewModel :INotifyPropertyChanged, IQueryAttributable
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private User userInfo;
		private ObservableCollection<User> userFriends;
		private ObservableCollection<LastestMessage> lastestMessages;

		private ServiceProvider _serviceProvider;

		public ListChatPageViewModel(ServiceProvider serviceProvider)
		{
			UserInfo = new User();
			UserFriends = new ObservableCollection<User>();
			LastestMessages = new ObservableCollection<LastestMessage>();
			this._serviceProvider = serviceProvider;
		}

		async Task GetListFriends() 
		{
			var response = await _serviceProvider.CallWebApi<int, ListChatInintializeResponse>
				("/ListChat/Initialize", HttpMethod.Post, UserInfo.Id);

			if (response.StatusCode == 200)
			{
				UserInfo = response.User;
				UserFriends = new ObservableCollection<User>(response.UserFriends);
				LastestMessages = new ObservableCollection<LastestMessage>(response.LastestMessages);
			}
			else
			{
				await AppShell.Current.DisplayAlert("ChatApp", response.StatusMessage, "OK");
			}
		}

		public void ApplyQueryAttributes(IDictionary<string, object> query)
		{
			if (query == null || query.Count == 0) return;

			UserInfo.Id = int.Parse(HttpUtility.UrlDecode(query["userId"].ToString()));
			Task.Run(async () =>
			{
				await GetListFriends();
			});
		}

		public User UserInfo
		{
			get { return userInfo; }
			set { userInfo = value; OnPropertyChanged(); }
		}

		public ObservableCollection<User> UserFriends
		{
			get { return userFriends; }
			set { userFriends = value; OnPropertyChanged(); }
		}

		public ObservableCollection<LastestMessage> LastestMessages
		{
			get { return lastestMessages; }
			set { lastestMessages = value; OnPropertyChanged(); }
		}

	
	}
}
