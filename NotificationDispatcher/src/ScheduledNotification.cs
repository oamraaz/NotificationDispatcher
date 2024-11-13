using System;

namespace NotificationDispatcher
{
    internal record ScheduledNotification
    {
        public DateTime ScheduledDeliveryTime { get; set; }
        public required Notification Notification { get; set; }
    }
}
