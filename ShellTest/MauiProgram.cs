using Microsoft.Extensions.Logging;
using ShellTest.Models;
using ShellTest.ViewModels;
using ShellTest.Views;

namespace ShellTest;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.RegisterViewsAndViewModels()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

	public static MauiAppBuilder RegisterViewsAndViewModels(this MauiAppBuilder mauiAppBuilder)
	{
		// PAGES AND VIEW MODELS
		mauiAppBuilder.Services.AddSingleton<SearchBarPage>();
		mauiAppBuilder.Services.AddSingleton<SearchBarViewModel>();
        mauiAppBuilder.Services.AddSingleton<SearchDetailPage>();
        mauiAppBuilder.Services.AddSingleton<SearchDetailViewModel>();
        mauiAppBuilder.Services.AddSingleton<FruitService>();

        return mauiAppBuilder;
	}
}