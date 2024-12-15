using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace catch_up_mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            //----------- Custom Section Start -----------
            // Konfiguracja HttpClient z ignorowaniem błędów certyfikatu SSL
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            builder.Services.AddScoped(sp => new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri("https://tmfgdsxq-7097.euw.devtunnels.ms/")
            });

            //Record Audio
            builder.Services.AddSingleton(AudioManager.Current);

            //File Saver
            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);

            // ----------- Custom Section End -----------

            return builder.Build();
        }
    }
}
