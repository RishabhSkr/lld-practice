using System;
namespace LldPractice.CSharp.ShoppingCart_Strategy;

public class CashPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Paid ₹{amount} using Cash.");
    }
}

public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Paid ₹{amount} using Credit Card.");
    }
}

public class UPIPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Paid ₹{amount} using UPI.");
    }
}


