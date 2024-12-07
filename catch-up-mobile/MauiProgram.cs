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
                BaseAddress = new Uri("https://localhost:7097/")
            });
            // ----------- Custom Section End -----------

            return builder.Build();
        }
    }
}
