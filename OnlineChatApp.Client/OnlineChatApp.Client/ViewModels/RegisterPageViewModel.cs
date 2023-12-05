using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChatApp.Client.ViewModels
{
	public class RegisterPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private ServiceProvider _serviceProvider;

		public RegisterPageViewModel(ServiceProvider serviceProvider)
		{
			this._serviceProvider = serviceProvider;
		}
	}
}
