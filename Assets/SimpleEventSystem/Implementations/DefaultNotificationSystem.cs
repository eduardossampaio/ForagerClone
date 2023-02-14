using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace SimpleEventSystem
{
    public class DefaultNotificationSystem : NotificationHandler
    {
        private Dictionary<string, ArrayList> notifications = new Dictionary<string, ArrayList>();

        public void ObserverEvent<T>(string eventName, NotificationEvent<T> notification)
        {
            if (notifications.ContainsKey(eventName))
            {
                AddExistingEvent(eventName, notification);
            }
            else
            {
                AddNewEvent(eventName, notification);
            }
        }

        public void UnregisterObserver<T>(NotificationEvent<T> notification)
        {
            UnregisterEvent(notification);
        }

        public void NotifyEvent<T>(string eventName, T parameters)
        {
            var notificationsList = notifications[eventName];
            if (notificationsList != null)
            {
                var priorityList = Enum.GetValues(typeof(NotificationEventPriority));
                Array.Reverse(priorityList);
                foreach (NotificationEventPriority priority in priorityList)
                {
                    NotifyByPriority(notificationsList, parameters, priority);
                }
            }
        }

        #region register events methods

        private void AddNewEvent<T>(string eventName, NotificationEvent<T> notification)
        {
            var notificationsList = new ArrayList();
            notificationsList.Add(notification);

            notifications[eventName] = notificationsList;
        }

        private void AddExistingEvent<T>(string eventName, NotificationEvent<T> notification)
        {

            var notificationsList = notifications[eventName];
            if (notificationsList != null)
            {
                notificationsList.Add(notification);
                //notificationsList.Sort(new SortByPriority());
            }
        }
        #endregion

        #region unregister event methods

        private void UnregisterEvent<T>(NotificationEvent<T> notification)
        {
            foreach(var entry in notifications)
            {
                var notificationsList = entry.Value;
                notificationsList.Remove(notification);
            }
        }

        #endregion

        #region notify events

        private void NotifyByPriority<T>(ArrayList notifications,T parameters, NotificationEventPriority priority)
        {
            foreach (var notification in notifications)
            {
                try
                {
                    var parsedNotification = (notification as NotificationEvent<T>);
                    if (parsedNotification.priority == priority) {
                        parsedNotification.action.Invoke(parameters);
                    }
                }
                catch (Exception)
                {

                }

            }
        }

        //public class SortByPriority : IComparer<NotificationEvent<?>>
        //{
        //    //public int Compare(object x, object y)
        //    //{
        //    //    if(x.GetType() == typeof(NotificationEvent<>) && y.GetType() == typeof(NotificationEvent<>))
        //    //    {
        //    //        var notificationX = (NotificationEvent<object>)x;
        //    //        var notificationY = (NotificationEvent<object>)y;

        //    //        return notificationX.priority - notificationY.priority;
        //    //    }
        //    //    return 0;
        //    //}
        //    public int Compare(SimpleEventSystem.NotificationEvent x, SimpleEventSystem.NotificationEvent y)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        #endregion
    }
}