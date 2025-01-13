using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using catch_up_mobile.SQLite;
using Plugin.Fingerprint;
using catch_up_mobile.Components;


namespace catch_up_mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitCamera()
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
            builder.Services.AddSingleton(sp => new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri("https://localhost:7097/")
            });

            //Record Audio
            builder.Services.AddSingleton(AudioManager.Current);

            //File Saver
            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);

            //SQL Lite
            builder.Services.AddSingleton<CatchUpLocalDb>(s => new CatchUpLocalDb(Path.Combine(FileSystem.AppDataDirectory, "CatchUpLocal.db3")));

            // Biometric Auth
            builder.Services.AddSingleton<IBiometricAuthService, BiometricAuthService>();
            

            // ----------- Custom Section End -----------
            #if ANDROID
             CrossFingerprint.SetCurrentActivityResolver(() => Platform.CurrentActivity);
            builder.Services.AddSingleton<ILightSensorService, LightSensorService>();

            #endif
            return builder.Build();
        }
    }
}
