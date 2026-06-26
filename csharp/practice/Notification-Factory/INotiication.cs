// using System;

// namespace Notification_Factory
// {
//     class SMSNotification : INotification
//     {
//         public string Target { get; private set; }

//         public SMSNotification(string target)
//         {
//             this.Target = target;
//         }

//         public void SendNotification()
//         {
//             Console.WriteLine($"Sending SMS to {Target}.");
//         }
//     }

//     // 3. Factory Class (Static)
//     static class NotificationFactory
//     {
//         // Return type is INotification
//         public static INotification CreateNotification(string type, string target)
//         {
//             if (type == "Email")
//                 return new EmailNotification(target);
//             else if (type == "SMS")
//                 return new SMSNotification(target);
//             // else if (type == "Push") ...
//             else
//                 throw new ArgumentException("Invalid notification type"); // Null dene se achha Exception throw karna hai
//         } 
//     }
// }