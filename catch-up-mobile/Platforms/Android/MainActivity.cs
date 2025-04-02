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
            RequestNotificationPermission();
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
            FirebaseCloudMessagingImplementation.SmallIconRef = Resource.Drawable.ic_notification;
        }

        private void RequestNotificationPermission()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu) // Android 13+
            {
                if (CheckSelfPermission(Android.Manifest.Permission.PostNotifications) != Permission.Granted)
                {
                    RequestPermissions(new[] { Android.Manifest.Permission.PostNotifications }, RequestNotificationPermissionCode);
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == RequestNotificationPermissionCode)
            {
                if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                {
                    Toast.MakeText(this, "Notifications are enabled.", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Notifications are disabled.", ToastLength.Short).Show();
                }
            }
        }
    }
}