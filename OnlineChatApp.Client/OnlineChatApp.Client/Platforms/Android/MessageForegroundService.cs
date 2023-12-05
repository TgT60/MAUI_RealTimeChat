using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidX.Core.App;

namespace OnlineChatApp.Client.Platforms.Android
{
	[Service(Label = "Message Foreground Service")]
	public class MessageForegroundService : Service
	{
		public override IBinder OnBind(Intent intent)
		{
			return null;
		}

		[return: GeneratedEnum]
		public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags,
			int startId)
		{
			CreateNotificationChannel();
			DispatchNotificationThatServiceIsRunning();

			return StartCommandResult.Sticky;
		}

		private const string SILENT_CHANNAL_ID = "999902";
		private const string SILENT_CHANNAL_NAME = "Message Foreground Service";
		void CreateNotificationChannel()
		{
			if (Build.VERSION.SdkInt < BuildVersionCodes.O)
			{
				return;
			}

			var channel = new NotificationChannel(SILENT_CHANNAL_ID, SILENT_CHANNAL_NAME,
				NotificationImportance.Default);

			channel.LockscreenVisibility = NotificationVisibility.Secret;
			channel.SetSound(null, null);
			channel.EnableVibration(false);	

			var notificationManager = GetSystemService(NotificationService) as NotificationManager;
			notificationManager.CreateNotificationChannel(channel);
		}

		void DispatchNotificationThatServiceIsRunning()
		{
			NotificationCompat.Builder builder = new NotificationCompat.Builder(
					this, SILENT_CHANNAL_ID)
				.SetContentTitle("Online Chat App")
				.SetContentText("Foreground service is running.")
				.SetSound(null)
				.SetVibrate(null)
				.SetSmallIcon(Resource.Drawable.dotnet_bot);

			StartForeground(1,builder.Build());
		}

	}
}
