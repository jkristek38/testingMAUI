using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using testMAUI.Models;
using testMAUI.ViewModels;
using testMAUI.Views;

namespace testMAUI;

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

        //builder.Services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

        builder.Services.AddSingleton<CartStore>();
        builder.Services.AddSingleton<InventoryStore>();
        builder.Services.AddSingleton<ItemStore>();

        builder.Services.AddTransient<GameViewModel>();
        builder.Services.AddTransient<GameView>((s) => new GameView()
        {
            BindingContext = s.GetRequiredService<GameViewModel>()
        });

        builder.Services.AddTransient<CartViewModel>();
        builder.Services.AddTransient<CartView>((s) => new CartView()
        {
            BindingContext = s.GetRequiredService<CartViewModel>()
        });
        builder.Services.AddTransient<InventoryViewModel>();
        builder.Services.AddTransient<InventoryView>((s) => new InventoryView()
        {
            BindingContext = s.GetRequiredService<InventoryViewModel>()
        });
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<LoginView>((s) => new LoginView()
        {
            BindingContext = s.GetRequiredService<LoginViewModel>()
        });
        builder.Services.AddTransient<DetailsViewModel>();
        builder.Services.AddTransient<DetailsView>((s) => new DetailsView()
        {
            BindingContext = s.GetRequiredService<DetailsViewModel>()
        });
        builder.Services.AddSingleton<TestViewModel>();
        builder.Services.AddSingleton<TestView>((s) => new TestView()
        {
            BindingContext = s.GetRequiredService<TestViewModel>()
        });
        builder.Services.AddSingleton<TestViewModel2>();
        builder.Services.AddSingleton<TestView2>((s) => new TestView2()
        {
            BindingContext = s.GetRequiredService<TestViewModel2>()
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
