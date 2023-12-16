using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Graphics.Drawables;
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
			UserInfo = new User();
			AllMembers = new ObservableCollection<User>();

			RefreshCommand = new Command( () =>
			{
				Task.Run(async () =>
				{
					IsRefreshing = true;
					await GetListMember();
				}).GetAwaiter().OnCompleted(() =>
				{
					IsRefreshing = false;
				});
			});

			_serviceProvider = serviceProvider;
		}

		private User userInfo;
		private ObservableCollection<User> allMembers { get; set; }
		private bool isRefreshing;

		async Task GetListMember()
		{
			var response = await _serviceProvider.CallWebApi<int, MemberResponse>
				("/Member/Members", HttpMethod.Post, UserInfo.Id);

			if (response.StatusCode == 200)
			{
				UserInfo = response.User;
				AllMembers = new ObservableCollection<User>(response.AllMembers);
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
		public ObservableCollection<User> AllMembers
		{
			get => allMembers;
			set { allMembers = value; OnPropertyChanged(); }
		}
		public bool IsRefreshing
		{
			get => isRefreshing;
			set { isRefreshing = value; OnPropertyChanged(); }
		}

		public ICommand RefreshCommand { get; set; }
	}
}
