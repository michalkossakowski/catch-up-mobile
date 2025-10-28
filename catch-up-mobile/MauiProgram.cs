using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using catch_up_mobile.SQLite;
using Plugin.LocalNotification;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using catch_up_mobile.Services;
using catch_up_mobile.Providers;
using Microsoft.Maui.Devices;
using System.Globalization;
using catch_up_mobile.Handlers;

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

            builder.Services.AddSingleton<CatchUpDbContext>(s => new CatchUpDbContext(Path.Combine(FileSystem.AppDataDirectory, "CatchUpLocal.db3")));
            builder.Services.AddTransient<catch_up_mobile.Handlers.AuthenticationHandler>();
            
            builder.Services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var dbContext = sp.GetRequiredService<CatchUpDbContext>();
                var baseAddress = configuration["ApiSettings:Url"];

                if (!string.IsNullOrWhiteSpace(baseAddress) && DeviceInfo.Platform == DevicePlatform.Android)
                {
                    try
                    {
                        var uri = new Uri(baseAddress);
                        if (uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase) || uri.Host.Equals("127.0.0.1"))
                        {
                            var uriBuilder = new UriBuilder(uri) { Host = "10.0.2.2" };
                            baseAddress = uriBuilder.Uri.ToString();
                        }
                    }
                    catch { }
                }

                var httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                var authHandler = new catch_up_mobile.Handlers.AuthenticationHandler(dbContext, configuration)
                {
                    InnerHandler = httpClientHandler
                };

                return new HttpClient(authHandler)
                {
                    BaseAddress = new Uri(baseAddress ?? throw new InvalidOperationException("API Url not found in configuration"))
                };
            });

            //Record Audio
            builder.Services.AddSingleton(AudioManager.Current);

            //File Saver
            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);

            // Biometric Auth
            builder.Services.AddSingleton<IBiometricAuthService, BiometricAuthService>();

#if ANDROID
            CrossFingerprint.SetCurrentActivityResolver(() => Platform.CurrentActivity);
            builder.Services.AddSingleton<ILightSensorService, LightSensorService>();
#endif
            //SignalR
            builder.Services.AddSingleton<SignalRService>();

            //HttpClient Provider with Authorization Token
            builder.Services.AddSingleton<HttpClientProvider>();

            //NotificationStateService
            builder.Services.AddSingleton<NotificationStateService>();
            
            // Firebase services
            builder = RegisterFirebaseServices(builder);

            //Localisation
            builder.Services.AddLocalization();
            //CultureInfo.CurrentCulture = new CultureInfo("pl-PL");
            //CultureInfo.CurrentUICulture = new CultureInfo("pl-PL");

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
