using Microsoft.Extensions.Logging;
using OnlineChatApp.Client.ViewModels;

namespace OnlineChatApp.Client
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
					fonts.AddFont("MaterialIcons-Regular.ttf", "IconFontTypes");
				});

			builder.Services.AddSingleton<AppShell>();
			builder.Services.AddSingleton<LoginPage>();
			builder.Services.AddSingleton<ListChatPage>();
			builder.Services.AddSingleton<LoginPageViewModel>();
			builder.Services.AddSingleton<ListChatPageViewModel>();
			builder.Services.AddSingleton<ServiceProvider>();

#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
