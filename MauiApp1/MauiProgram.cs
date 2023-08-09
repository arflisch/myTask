using MauiApp1.ViewModel;
using Microsoft.Extensions.Logging;
using Realms.Exceptions;

namespace MauiApp1;

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
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<DetailPage>();
        builder.Services.AddTransient<DetailViewModel>();

        var app = builder.Build();
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var config = new RealmConfiguration(path + "my.realm")
        {
            IsReadOnly = false,
        };
        Realm localRealm;
        try
        {
            localRealm = Realm.GetInstance(config);
        }
        catch (RealmFileAccessErrorException ex)
        {
            Console.WriteLine($@"Error creating or opening the
        realm file. {ex.Message}");
        }

        return app;
	}
}
