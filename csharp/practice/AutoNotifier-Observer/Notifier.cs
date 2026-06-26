/*
    // Real-world: Bank mein transaction hone pe kya kya hota hai?
// 1. SMS aata hai         → "₹5000 credited to your account"
// 2. Email aata hai       → "Transaction alert: ₹5000 deposited"
// 3. Transaction log hota → "[LOG] Deposit ₹5000 to Amit's account"

// Step 1: Observer Interface
interface ITransactionObserver
{
    void OnTransaction(string accountHolder, string type, double amount, double newBalance);
    // type = "Deposit" ya "Withdraw"
}

// Step 2: 3 Observers banao
// SMSNotifier     → "📱 SMS to Amit: ₹5000 deposited. Balance: ₹15000"
// EmailNotifier   → "📧 Email to Amit: Transaction Alert - ₹5000 deposited"
// TransactionLogger → "📝 [LOG] 2025-06-26 11:00:00 | Amit | Deposit | ₹5000 | Balance: ₹15000"

// Step 3: BankAccount class mein Observer pattern add karo
class BankAccount
{
    string ownerName;
    double balance;
    List<ITransactionObserver> observers = new();

    void Subscribe(ITransactionObserver observer);
    void Unsubscribe(ITransactionObserver observer);
    
    void Deposit(double amount);   // deposit + notify all observers
    void Withdraw(double amount);  // withdraw + notify all observers
}

// Step 4: Main mein test karo
var acc = new BankAccount("Amit", 10000);

var sms = new SMSNotifier();
var email = new EmailNotifier();
var logger = new TransactionLogger();

acc.Subscribe(sms);
acc.Subscribe(email);
acc.Subscribe(logger);

acc.Deposit(5000);
// Teeno ko notify hona chahiye

Console.WriteLine();

acc.Unsubscribe(email);   // Email band karo
acc.Withdraw(3000);
// Sirf SMS aur Logger ko notify hoga (Email nahi)
*/
using System;

namespace LldPractice.CSharp.AutoNotifier_Observer;


public enum NotifierType
{   
    WITHDRAW,
    DEPOSIT
}

public interface ITransactionObserver
{
    void OnTransaction(string accountHolder, NotifierType type, double amt, double newBalance);
}

public class SMSNotifier : ITransactionObserver
{ 
    public void OnTransaction(string accountHolder, NotifierType type, double amt, double newBalance)
    {
        Console.WriteLine($"📱 SMS to {accountHolder}: {amt} {type}ed. Balance: {newBalance}");
    }
}

public class EmailNotifier : ITransactionObserver
{ 
    public void OnTransaction(string accountHolder, NotifierType type, double amt, double newBalance)
    {
        Console.WriteLine($"📧 Email to {accountHolder}: {amt} {type}ed. Balance: {newBalance}");
    }
}

public class TransactionLogger : ITransactionObserver
{ 
    public void OnTransaction(string accountHolder, NotifierType type, double amt, double newBalance)
    {
        Console.WriteLine($"[LOG] for {accountHolder}: {amt} {type}ed. Balance: {newBalance}");
    }
}



