using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using catch_up_mobile.SQLite;
using Plugin.LocalNotification;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Extensions.Configuration;
using System.Reflection;

#if ANDROID
using Plugin.Fingerprint;
using catch_up_mobile.Components;
using Plugin.Firebase.Core.Platforms.Android;
#endif

namespace catch_up_mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
           
            using var secretsStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("catch_up_mobile.secrets.json");
            if (secretsStream == null)
            {
                throw new FileNotFoundException("Embedded resource 'secrets.json' not found.");
            }
            builder.Configuration.AddJsonStream(secretsStream);

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitCamera()
                .UseMauiCommunityToolkit()
                .UseLocalNotification()
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

            // HttpClient with ignoring SSL
            builder.Services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var baseAddress = configuration["ApiSettings:Url"];

                var httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                return new HttpClient(httpClientHandler)
                {
                    BaseAddress = new Uri(baseAddress ?? throw new InvalidOperationException("API Url not found in configuration"))
                };
            });

            //Record Audio
            builder.Services.AddSingleton(AudioManager.Current);

            //File Saver
            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);

            //SQL Lite
            builder.Services.AddSingleton<CatchUpLocalDb>(s => new CatchUpLocalDb(Path.Combine(FileSystem.AppDataDirectory, "CatchUpLocal.db3")));

            // Biometric Auth
            builder.Services.AddSingleton<IBiometricAuthService, BiometricAuthService>();

#if ANDROID
            CrossFingerprint.SetCurrentActivityResolver(() => Platform.CurrentActivity);
            builder.Services.AddSingleton<ILightSensorService, LightSensorService>();
#endif

            // ----------- Custom Section End -----------
            return builder.Build();
        }
        private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events => {
#if ANDROID
                events.AddAndroid(android => android.OnCreate((activity, _) =>
                CrossFirebase.Initialize(activity)));
#endif
            });

            return builder;
        }
    }
}
