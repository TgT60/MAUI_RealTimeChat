using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChatApp.Client.ViewModels
{
    public class ChatPageViewModel : INotifyPropertyChanged, IQueryAttributable
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void ApplyQueryAttributes(IDictionary<string, object> query)
		{
			if (query == null || query.Count == 0) return;

			FromUserId = int.Parse(HttpUtility.UrlDecode(query["fromUserId"].ToString()));
			ToUserId = int.Parse(HttpUtility.UrlDecode(query["toUserId"].ToString()));

		}

		private ServiceProvider _serviceProvider;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public ChatPageViewModel(ServiceProvider serviceProvider)
		{
			Messages = new ObservableCollection<Message>();
			_serviceProvider = serviceProvider;
		}

		async Task GetMessages()
		{
			var requset = new MessageInitializeRequest
			{
				FromUserId = FromUserId,
				ToUserId = ToUserId,
			};

			var response = await _serviceProvider.CallWebApi<MessageInitializeRequest, MessageInitializeResponse>
				("/Message/Initialize", HttpMethod.Post, requset);

			if (response.StatusCode == 200)
			{
				FriendInfo = response.FriendInfo;
				Messages = new ObservableCollection<Message>(response.Messages);
				
			}
			else
			{
				await AppShell.Current.DisplayAlert("ChatApp", response.StatusMessage, "OK");
			}
		}

		public void Initialize()
		{
			isRefreshing = false;
			Task.Run(async () =>
			{
				isRefreshing = true;	
				await GetMessages();
			}).GetAwaiter().OnCompleted(() =>
			{
				isRefreshing = false;
			});
		}

		private int fromUserId;
		private int toUserId;
		private User friendInfo;
		private ObservableCollection<Message> messages;
		private bool isRefreshing;

		public int FromUserId
		{
			get { return fromUserId; }
			set { fromUserId = value; OnPropertyChanged(); }
		}

		public int ToUserId
		{
			get { return toUserId; }
			set { toUserId = value; OnPropertyChanged(); }
		}

		public User FriendInfo
		{
			get { return friendInfo; }
			set { friendInfo = value; OnPropertyChanged(); }
		}

		public ObservableCollection<Message> Messages
		{
			get { return messages; }
			set { messages = value; OnPropertyChanged();}
		}

		public bool IsRefreshing
		{
			get { return isRefreshing;}
			set { isRefreshing = value; OnPropertyChanged(); }
		}
	}
}
