using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SimpleEventSystem
{

    public class NotificationCenter : MonoBehaviour, NotificationHandler
    {
        private NotificationHandler notificationHandler = new DefaultNotificationSystem();

        public void NotifyEvent<T>(string eventName, T parameters)
        {
            notificationHandler.NotifyEvent(eventName, parameters);
        }

        public void ObserverEvent<T>(string eventName, NotificationEvent<T> notification)
        {
            notificationHandler.ObserverEvent(eventName, notification);
        }

        public void UnregisterObserver<T>(NotificationEvent<T> notification)
        {
            notificationHandler.UnregisterObserver(notification);
        }
    }
}