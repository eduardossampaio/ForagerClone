using UnityEngine;
using System;

namespace SimpleEventSystem
{
    public class NotificationEvent<T>
    {
        public NotificationEventPriority priority { get; set; }
        public Action<T> action { get; set; }

        public NotificationEvent(NotificationEventPriority priority, Action<T> action)
        {
            this.priority = priority;
            this.action = action;
        }

    }
}
