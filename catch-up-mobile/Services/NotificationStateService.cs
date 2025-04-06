namespace catch_up_mobile.Services
{
    public class NotificationStateService
    {
        public bool UnreadNotification { get; private set; } = false;

        public event Func<Task> OnChange;

        public void SetUnreadNotification(bool value)
        {
            UnreadNotification = value;
            NotifyStateChanged();
        }

        private async void NotifyStateChanged()
        {
            if (OnChange != null)
            {
                await OnChange.Invoke();
            }
        }
    }
}