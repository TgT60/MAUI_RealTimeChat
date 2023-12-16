using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineChatApp.Client.Services.Member;

namespace OnlineChatApp.Client.ViewModels
{
	public class MemberPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private ServiceProvider _serviceProvider;

		public MemberPageViewModel(ServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		private User userInfo;
		private ObservableCollection<User> members { get; set; }
		private bool isRefreshing;

		async Task GetListMember()
		{

			var response = await _serviceProvider.CallWebApi<int, MemberResponse>
				("/Member/Members", HttpMethod.Post, UserInfo.Id);

			if (response.StatusCode == 200)
			{
				UserInfo = response.User;
				Members = new ObservableCollection<User>(response.AllMembers);
				//	LastestMessages = new ObservableCollection<LastestMessage>(response.LastestMessages);
			}
			else
			{
				await AppShell.Current.DisplayAlert("ChatApp", response.StatusMessage, "OK");
			}

		}

		public void Initialize()
		{
			Task.Run(async () =>
			{
				IsRefreshing = true;
				await GetListMember();
			}).GetAwaiter().OnCompleted(() =>
			{
				IsRefreshing = false;
			});
		}

		public User UserInfo
		{
			get => userInfo;
			set { userInfo = value; OnPropertyChanged(); }
		}
		public ObservableCollection<User> Members
		{
			get => members;
			set { members = value; OnPropertyChanged(); }
		}
		public bool IsRefreshing
		{
			get => isRefreshing;
			set { isRefreshing = value; OnPropertyChanged(); }
		}
	}
}
