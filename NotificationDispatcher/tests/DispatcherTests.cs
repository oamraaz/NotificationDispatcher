using System;
using Xunit;

namespace NotificationDispatcher.Tests
{
    public class DispatcherTests
    {
        private readonly int day = 1;
        private readonly int month = 2;
        private readonly int year = 2023;
        private readonly string[] accounts;

        public DispatcherTests()
        {
            accounts = [
                "test_1@fake.tg",
                "test_2@fake.tg",
                "test_3@fake.tg",
                "test_4@fake.tg",
                "test_5@fake.tg",
                "test_6@fake.tg",
                "test_7@fake.tg"
            ];
        }

        [Fact]
        public void Test_1()
        {
            var dispatcher = new Dispatcher();

            dispatcher.PushNotification(new Notification(1, accounts[0], new DateTime(year, month, day, 12, 0, 0), NotificationPriority.High));
            dispatcher.PushNotification(new Notification(2, accounts[1], new DateTime(year, month, day, 12, 1, 0), NotificationPriority.High));
            dispatcher.PushNotification(new Notification(3, accounts[0], new DateTime(year, month, day, 12, 1, 25), NotificationPriority.Low));
            dispatcher.PushNotification(new Notification(4, accounts[1], new DateTime(year, month, day, 12, 1, 35), NotificationPriority.Low));
            dispatcher.PushNotification(new Notification(5, accounts[0], new DateTime(year, month, day, 12, 1, 50), NotificationPriority.Low));
            dispatcher.PushNotification(new Notification(6, accounts[1], new DateTime(year, month, day, 12, 2, 0), NotificationPriority.High));
            dispatcher.PushNotification(new Notification(7, accounts[0], new DateTime(year, month, day, 12, 2, 1), NotificationPriority.High));

            var results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 2 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[2].Notification.Id == 3 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 25));
            Assert.True(results[3].Notification.Id == 4 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 2, 0));
            Assert.True(results[4].Notification.Id == 7 && results[4].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 2, 25));
            Assert.True(results[5].Notification.Id == 6 && results[5].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 3, 0));
            Assert.True(results[6].Notification.Id == 5 && results[6].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 25));
        }

        [Fact]
        public void Test_2()
        {
            var dispatcher = new Dispatcher();

            dispatcher.PushNotification(new Notification(1, accounts[0], new DateTime(year, month, day, 12, 0, 0), NotificationPriority.High));
            dispatcher.PushNotification(new Notification(2, accounts[1], new DateTime(year, month, day, 12, 1, 0), NotificationPriority.High));
            dispatcher.PushNotification(new Notification(3, accounts[0], new DateTime(year, month, day, 12, 1, 25), NotificationPriority.Low));
            dispatcher.PushNotification(new Notification(4, accounts[1], new DateTime(year, month, day, 12, 1, 35), NotificationPriority.Low));
            dispatcher.PushNotification(new Notification(5, accounts[0], new DateTime(year, month, day, 12, 1, 50), NotificationPriority.Low));

            var results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 2 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[2].Notification.Id == 3 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 25));
            Assert.True(results[3].Notification.Id == 4 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 2, 0));
            Assert.True(results[4].Notification.Id == 5 && results[4].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 25));

            dispatcher.PushNotification(new Notification(6, accounts[1], new DateTime(year, month, day, 12, 2, 0), NotificationPriority.High));
            dispatcher.PushNotification(new Notification(7, accounts[0], new DateTime(year, month, day, 12, 2, 1), NotificationPriority.High));

            results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 2 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[2].Notification.Id == 3 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 25));
            Assert.True(results[3].Notification.Id == 4 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 2, 0));
            Assert.True(results[4].Notification.Id == 7 && results[4].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 2, 25));
            Assert.True(results[5].Notification.Id == 6 && results[5].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 3, 0));
            Assert.True(results[6].Notification.Id == 5 && results[6].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 25));
        }

        [Fact]
        public void Test_3()
        {
            var dispatcher = new Dispatcher();

            dispatcher.PushNotification(new Notification(1, accounts[0], new DateTime(year, month, day, 12, 0, 0), NotificationPriority.High));
            dispatcher.PushNotification(new Notification(2, accounts[1], new DateTime(year, month, day, 12, 1, 0), NotificationPriority.High));
            dispatcher.PushNotification(new Notification(3, accounts[0], new DateTime(year, month, day, 12, 1, 25), NotificationPriority.Low));
            dispatcher.PushNotification(new Notification(4, accounts[1], new DateTime(year, month, day, 12, 1, 35), NotificationPriority.Low));
            dispatcher.PushNotification(new Notification(5, accounts[0], new DateTime(year, month, day, 12, 1, 50), NotificationPriority.Low));

            var results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 2 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[2].Notification.Id == 3 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 25));
            Assert.True(results[3].Notification.Id == 4 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 2, 0));
            Assert.True(results[4].Notification.Id == 5 && results[4].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 25));

            dispatcher.PushNotification(new Notification(6, accounts[1], new DateTime(year, month, day, 12, 2, 0), NotificationPriority.High));

            results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 2 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[2].Notification.Id == 3 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 25));
            Assert.True(results[3].Notification.Id == 4 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 2, 0));
            Assert.True(results[4].Notification.Id == 6 && results[4].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 3, 0));
            Assert.True(results[5].Notification.Id == 5 && results[5].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 25));

            dispatcher.PushNotification(new Notification(7, accounts[0], new DateTime(year, month, day, 12, 2, 1), NotificationPriority.High));

            results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 2 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[2].Notification.Id == 3 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 25));
            Assert.True(results[3].Notification.Id == 4 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 2, 0));
            Assert.True(results[4].Notification.Id == 7 && results[4].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 2, 25));
            Assert.True(results[5].Notification.Id == 6 && results[5].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 3, 0));
            Assert.True(results[6].Notification.Id == 5 && results[6].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 25));
        }

        [Fact]
        public void Test_4()
        {
            var dispatcher = new Dispatcher();

            dispatcher.PushNotification(new Notification(1, accounts[0], new DateTime(year, month, day, 12, 0, 0), NotificationPriority.High));
            dispatcher.PushNotification(new Notification(2, accounts[0], new DateTime(year, month, day, 12, 0, 1), NotificationPriority.Low));
            dispatcher.PushNotification(new Notification(3, accounts[0], new DateTime(year, month, day, 12, 0, 2), NotificationPriority.Low));

            var results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 2 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[2].Notification.Id == 3 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 0));

            dispatcher.PushNotification(new Notification(4, accounts[1], new DateTime(year, month, day, 12, 0, 3), NotificationPriority.High));

            results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 4 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 10));
            Assert.True(results[2].Notification.Id == 2 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[3].Notification.Id == 3 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 0));
        }

        [Fact]
        public void Test_5()
        {
            var dispatcher = new Dispatcher();

            dispatcher.PushNotification(new Notification(1, accounts[0], new DateTime(year, month, day, 12, 0, 0), NotificationPriority.High));
            dispatcher.PushNotification(new Notification(2, accounts[0], new DateTime(year, month, day, 12, 0, 1), NotificationPriority.Low));
            dispatcher.PushNotification(new Notification(3, accounts[0], new DateTime(year, month, day, 12, 0, 2), NotificationPriority.Low));

            var results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 2 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[2].Notification.Id == 3 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 0));

            dispatcher.PushNotification(new Notification(4, accounts[2], new DateTime(year, month, day, 12, 0, 2, 1), NotificationPriority.High));

            results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 4 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 10));
            Assert.True(results[2].Notification.Id == 2 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[3].Notification.Id == 3 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 0));

            dispatcher.PushNotification(new Notification(5, accounts[1], new DateTime(year, month, day, 12, 0, 3), NotificationPriority.High));

            results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 4 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 10));
            Assert.True(results[2].Notification.Id == 5 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 20));
            Assert.True(results[3].Notification.Id == 2 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[4].Notification.Id == 3 && results[4].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 0));
        }

        [Fact]
        public void Test_6()
        {
            var dispatcher = new Dispatcher();

            dispatcher.PushNotification(new Notification(1, accounts[0], new DateTime(year, month, day, 12, 0, 0), NotificationPriority.High));
            dispatcher.PushNotification(new Notification(2, accounts[0], new DateTime(year, month, day, 12, 0, 1), NotificationPriority.Low));
            dispatcher.PushNotification(new Notification(3, accounts[0], new DateTime(year, month, day, 12, 0, 2), NotificationPriority.Low));

            var results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 2 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[2].Notification.Id == 3 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 0));

            dispatcher.PushNotification(new Notification(4, accounts[2], new DateTime(year, month, day, 12, 0, 2, 1), NotificationPriority.High));

            results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 4 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 10));
            Assert.True(results[2].Notification.Id == 2 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[3].Notification.Id == 3 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 0));

            dispatcher.PushNotification(new Notification(5, accounts[1], new DateTime(year, month, day, 12, 0, 3), NotificationPriority.Low));

            results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 4 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 10));
            Assert.True(results[2].Notification.Id == 5 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 20));
            Assert.True(results[3].Notification.Id == 2 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[4].Notification.Id == 3 && results[4].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 0));
        }
        [Fact]
        public void Test_7()
        {
            var dispatcher = new Dispatcher();

            dispatcher.PushNotification(new Notification(1, accounts[0], new DateTime(year, month, day, 12, 0, 0), NotificationPriority.High));
            dispatcher.PushNotification(new Notification(2, accounts[0], new DateTime(year, month, day, 12, 0, 1), NotificationPriority.Low));
            dispatcher.PushNotification(new Notification(3, accounts[0], new DateTime(year, month, day, 12, 0, 2), NotificationPriority.Low));

            var results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 2 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[2].Notification.Id == 3 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 0));

            dispatcher.PushNotification(new Notification(4, accounts[2], new DateTime(year, month, day, 12, 0, 2, 1), NotificationPriority.High));

            results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 4 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 10));
            Assert.True(results[2].Notification.Id == 2 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[3].Notification.Id == 3 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 0));

            dispatcher.PushNotification(new Notification(5, accounts[1], new DateTime(year, month, day, 12, 0, 3), NotificationPriority.Low));

            results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 4 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 10));
            Assert.True(results[2].Notification.Id == 5 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 20));
            Assert.True(results[3].Notification.Id == 2 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[4].Notification.Id == 3 && results[4].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 0));

            dispatcher.PushNotification(new Notification(6, accounts[1], new DateTime(year, month, day, 12, 0, 4), NotificationPriority.Low));

            results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 4 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 10));
            Assert.True(results[2].Notification.Id == 5 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 20));
            Assert.True(results[3].Notification.Id == 2 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[4].Notification.Id == 6 && results[4].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 0, 20));
            Assert.True(results[5].Notification.Id == 3 && results[5].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 0));

            dispatcher.PushNotification(new Notification(7, accounts[1], new DateTime(year, month, day, 12, 0, 5), NotificationPriority.Low));

            results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 4 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 10));
            Assert.True(results[2].Notification.Id == 5 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 20));
            Assert.True(results[3].Notification.Id == 2 && results[3].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 1, 0));
            Assert.True(results[4].Notification.Id == 6 && results[4].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 0, 20));
            Assert.True(results[5].Notification.Id == 3 && results[5].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 1, 0));
            Assert.True(results[6].Notification.Id == 7 && results[6].ScheduledDeliveryTime == new DateTime(year, month, day + 2, 12, 0, 20));
        }

        [Fact]
        public void Test_8()
        {
            var dispatcher = new Dispatcher();

            dispatcher.PushNotification(new Notification(1, accounts[0], new DateTime(year, month, day, 12, 0, 0), NotificationPriority.Low));
            dispatcher.PushNotification(new Notification(2, accounts[0], new DateTime(year, month, day, 20, 0, 0), NotificationPriority.Low));
            dispatcher.PushNotification(new Notification(3, accounts[0], new DateTime(year, month, day + 3, 3, 0, 2), NotificationPriority.Low));

            var results = dispatcher.GetOrderedNotifications();

            Assert.True(results[0].Notification.Id == 1 && results[0].ScheduledDeliveryTime == new DateTime(year, month, day, 12, 0, 0));
            Assert.True(results[1].Notification.Id == 2 && results[1].ScheduledDeliveryTime == new DateTime(year, month, day + 1, 12, 0, 0));
            Assert.True(results[2].Notification.Id == 3 && results[2].ScheduledDeliveryTime == new DateTime(year, month, day + 3, 3, 0, 2));
        }
    }
}