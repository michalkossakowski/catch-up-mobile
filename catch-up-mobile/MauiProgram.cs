using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
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
                BaseAddress = new Uri("https://localhost:7097/")
            });
            // ----------- Custom Section End -----------

            return builder.Build();
        }
    }
}
