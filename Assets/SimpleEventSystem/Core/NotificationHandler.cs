using UnityEngine;


namespace SimpleEventSystem
{
    public interface NotificationHandler
    {
        public void ObserverEvent<T>(string eventName, NotificationEvent<T> notification);

        public void UnregisterObserver<T>(NotificationEvent<T> notification);

        public void NotifyEvent<T>(string eventName, T parameters);
    }
}