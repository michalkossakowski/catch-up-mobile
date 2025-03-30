using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Plugin.Firebase.CloudMessaging;

namespace catch_up_mobile
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public static MainActivity Instance { get; private set; } = null!;
        private const int RequestNotificationPermissionCode = 100;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Instance = this;
            HandleIntent(Intent);
            CreateNotificationChannelIfNeeded();
            RequestNotificationPermission(); // Dodajemy prośbę o uprawnienia
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            HandleIntent(intent);
        }

        private static void HandleIntent(Intent intent)
        {
            FirebaseCloudMessagingImplementation.OnNewIntent(intent);
        }

        private void CreateNotificationChannelIfNeeded()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                CreateNotificationChannel();
            }
        }

        private void CreateNotificationChannel()
        {
            var channelId = $"{PackageName}.general";
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            var channel = new NotificationChannel(channelId, "General", NotificationImportance.Default);
            notificationManager.CreateNotificationChannel(channel);
            FirebaseCloudMessagingImplementation.ChannelId = channelId;
        }

        // Metoda do żądania uprawnień do powiadomień
        private void RequestNotificationPermission()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu) // Android 13+
            {
                if (CheckSelfPermission(Android.Manifest.Permission.PostNotifications) != Permission.Granted)
                {
                    // Żądamy uprawnienia, jeśli nie jest przyznane
                    RequestPermissions(new[] { Android.Manifest.Permission.PostNotifications }, RequestNotificationPermissionCode);
                }
            }
        }

        // Obsługa wyniku żądania uprawnień
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == RequestNotificationPermissionCode)
            {
                if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                {
                    // Uprawnienie przyznane
                    Android.Util.Log.Info("NotificationPermission", "Notification permission granted");
                }
                else
                {
                    // Uprawnienie odrzucone
                    Android.Util.Log.Info("NotificationPermission", "Notification permission denied");
                    // Możesz tutaj dodać komunikat dla użytkownika, np. toast
                    Toast.MakeText(this, "Powiadomienia są wymagane do pełnego działania aplikacji.", ToastLength.Long).Show();
                }
            }
        }
    }
}