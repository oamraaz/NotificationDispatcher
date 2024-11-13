using System;

namespace NotificationDispatcher
{
    internal class Notification(int id, string messengerAccount, DateTime created, NotificationPriority priority)
    {
        public int Id { get; set; } = id;
        public string MessengerAccount { get; set; } = messengerAccount;
        public string Message { get; set; } = "Hello, world!";
        public NotificationPriority Priority { get; set; } = priority;
        public DateTime Created { get; set; } = created;
        public DateTime Expired { get; set; } = CalculateExpiredDate(created, priority);

        private static DateTime CalculateExpiredDate(DateTime created, NotificationPriority priority)
        {
            return priority == NotificationPriority.High
                ? created.AddHours(1)
                : created.AddHours(24);
        }
    }
}
