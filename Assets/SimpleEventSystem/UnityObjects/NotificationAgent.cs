using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SimpleEventSystem
{
    [System.Serializable]
    class EventSystemNotification
    {
        public string eventName;
        public NotificationEventPriority priority = NotificationEventPriority.NORMAL;
        public UnityEvent<object> actions;
    }

    public class NotificationAgent : MonoBehaviour
    {
        private NotificationCenter notificationCenter;
        /*[SerializeField]*/ private EventSystemNotification[] listenningNotifications;

        private List<NotificationEvent<object>> notificationEvents;

        private void Awake()
        {          
            notificationCenter = FindObjectOfType<NotificationCenter>();
            if(notificationCenter == null)
            {
                CreateNotificationCenter();
            }
        }

        private void Start()
        {
            RegisterNotifications();
        }

        private void OnDestroy()
        {
            UnRegisterNotifications();
        }


        private void CreateNotificationCenter()
        {
            var notificationCenterObj = new GameObject("NotificationCenter");
            notificationCenter = notificationCenterObj.AddComponent<NotificationCenter>();
        }

        //TODO este método está dando erro
        private void RegisterNotifications()
        {
            //notificationEvents = new List<NotificationEvent<object>>();
            //foreach (var notification in listenningNotifications)
            //{
            //    var notificationEvent = new NotificationEvent<object>(notification.priority, (parameter) =>
            //    {
            //        notification.actions.Invoke(parameter);
            //    });
            //    notificationEvents.Add(notificationEvent);
            //    notificationCenter.ObserverEvent(notification.eventName, notificationEvent);
            //}
        }

        public void NotifyEvent<T>(string eventName, T parameter)
        {
            notificationCenter.NotifyEvent(eventName, parameter);
        }

        public void RegisterEvent<T> (string eventName, Action<T> action, NotificationEventPriority priority = NotificationEventPriority.NORMAL)
        {
            NotificationEvent<T> notificationEvent = new NotificationEvent<T>(priority, action);
            notificationCenter.ObserverEvent(eventName, notificationEvent);
        }
        private void UnRegisterNotifications()
        {
            if(notificationEvents == null)
            {
                return;
            }
            foreach (var notification in notificationEvents)
            {
                notificationCenter.UnregisterObserver(notification);
            }
        }

    }
}