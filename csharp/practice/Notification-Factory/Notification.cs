using System;

namespace Notification_Factory;

public interface INotification
{
    void Notify();
}


static class NotificationFactory
{
    public static INotification CreateNotification(string type, string target)
    {
        // return a object of type 
        if (type == "Email")
            return new EmailNotification(target);
        else if (type == "SMS")
            return new SMSNotification(target);
        else if (type == "Push"){
            return new PushNotification(target);
        }
        else
            return null;
    } 
}

public class EmailNotification: INotification
{
    public string Target { get; private set; }

    public EmailNotification(string target)
    {
        this.Target=target;
    }
    public void  Notify()
    {
        Console.WriteLine($"Sending email to {Target}.");
    }
}

public class SMSNotification: INotification
{
    public string Target { get; private set; }

    public SMSNotification(string target)
    {
        this.Target=target;
    }
    public void  Notify()
    {
        Console.WriteLine($"Sending SMS to {Target}."); 
    }

}

public class PushNotification: INotification
{
    public string Target { get; private set; }

    public PushNotification(string target)    
    {
        this.Target=target;
    }
    public void  Notify()
    {
        Console.WriteLine($"Sending Push Notification to {Target}."); 
    }
}