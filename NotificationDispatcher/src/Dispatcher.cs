using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NotificationDispatcher
{
    internal class Dispatcher
    {
        private readonly List<ScheduledNotification> _notifications = new();

        public void PushNotification(Notification notification)
        {
            var scheduledTime = CalculateScheduledTime(notification);
            _notifications.Add(new ScheduledNotification
            {
                Notification = notification,
                ScheduledDeliveryTime = scheduledTime
            });
        }

        public ReadOnlyCollection<ScheduledNotification> GetOrderedNotifications()
        {
            return _notifications
                .OrderBy(n => n.ScheduledDeliveryTime)
                .ToList()
                .AsReadOnly();
        }

        private DateTime CalculateScheduledTime(Notification notification)
        {
            if (_notifications.Count == 0)
                return notification.Created;

            var scheduledTime = notification.Created;

            var accountNotifications = _notifications
                .Where(n => n.Notification.MessengerAccount == notification.MessengerAccount)
                .OrderBy(n => n.ScheduledDeliveryTime)
                .ToList();

            if (accountNotifications.Count != 0)
            {
                if (notification.Priority == NotificationPriority.Low)
                {
                    var lastLowPriority = accountNotifications
                        .LastOrDefault(n => n.Notification.Priority == NotificationPriority.Low);

                    scheduledTime = lastLowPriority != null
                        ? AdjustScheduledTimeForLowPriority(notification, lastLowPriority)
                        : EnsureMinimumScheduledTime(accountNotifications, scheduledTime);
                }
                else
                {
                    scheduledTime =
                        AdjustScheduledTimeForHighPriority(notification, accountNotifications, scheduledTime);
                }
            }

            scheduledTime = AdjustScheduledTimeForOtherAccounts(notification, scheduledTime);

            return scheduledTime;
        }

        private DateTime AdjustScheduledTimeForOtherAccounts(Notification notification, DateTime scheduledTime)
        {
            var otherTimes = _notifications
                .Where(n => n.Notification.MessengerAccount != notification.MessengerAccount)
                .OrderBy(n => n.ScheduledDeliveryTime)
                .Select(n => n.ScheduledDeliveryTime);

            foreach (var otherTime in otherTimes)
            {
                var timeDiff = Math.Abs((scheduledTime - otherTime).TotalSeconds);
                if (!(timeDiff < 10)) continue;
                scheduledTime = otherTime.AddSeconds(10);
            }

            return scheduledTime;
        }

        private static DateTime AdjustScheduledTimeForHighPriority(Notification notification,
            List<ScheduledNotification> accountNotifications,
            DateTime scheduledTime)
        {
            var lastRelevantNotification = accountNotifications
                .Where(n => n.Notification.Priority == NotificationPriority.High ||
                            (n.Notification.Priority == NotificationPriority.Low &&
                             n.ScheduledDeliveryTime.Date == notification.Created.Date))
                .MaxBy(n => n.ScheduledDeliveryTime);

            if (lastRelevantNotification != null)
            {
                var minTime = lastRelevantNotification.ScheduledDeliveryTime.AddMinutes(1);
                scheduledTime = scheduledTime > minTime ? scheduledTime : minTime;
            }

            return scheduledTime;
        }

        private static DateTime EnsureMinimumScheduledTime(List<ScheduledNotification> accountNotifications,
            DateTime scheduledTime)
        {
            var lastTime = accountNotifications.Last().ScheduledDeliveryTime;
            var minTime = lastTime.AddMinutes(1);
            scheduledTime = scheduledTime > minTime ? scheduledTime : minTime;
            return scheduledTime;
        }

        private static DateTime AdjustScheduledTimeForLowPriority(Notification notification,
            ScheduledNotification lastLowPriority)
        {
            DateTime scheduledTime;
            var timeSinceLastLow = notification.Created - lastLowPriority.ScheduledDeliveryTime;

            if (timeSinceLastLow.TotalHours >= 24)
            {
                scheduledTime = notification.Created;
            }
            else
            {
                scheduledTime = lastLowPriority.ScheduledDeliveryTime.Date.AddDays(1)
                    .Add(lastLowPriority.ScheduledDeliveryTime.TimeOfDay);
            }

            return scheduledTime;
        }
    }
}