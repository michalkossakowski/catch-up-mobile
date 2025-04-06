
using catch_up_mobile.Dtos;

namespace catch_up_mobile.States
{
    public class NotificationState
    {
        public event Action OnChange;
        private int _unreadCount;

        public int UnreadCount
        {
            get => _unreadCount;
            private set
            {
                _unreadCount = value;
                NotifyStateChanged();
            }
        }

        public void AddNotification(NotificationDto notification)
        {
            if (!notification.IsRead)
            {
                UnreadCount++;
            }
        }

        public void ResetUnreadCount()
        {
            UnreadCount = 0;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
