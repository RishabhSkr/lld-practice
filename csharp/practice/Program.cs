using System;
using LldPractice.CSharp.Bank;
using LldPractice.CSharp.ShoppingCart_Strategy;
using LldPractice.CSharp.LoggerSingleton;
using Notification_Factory;
using LldPractice.CSharp.ConfigurationManagerSingleton;
using LldPractice.CSharp.AutoNotifier_Observer;     
using LldPractice.CSharp.Loan_StatePattern;

namespace LldPractice.CSharp
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("🏦 === BANK SYSTEM ===\n");
        // POLYMORPHISM — IAccount type mein koi bhi account aa sakta hai
        IAccount savings = new SavingsAccount(101, 10000,"Amit");
        IAccount current = new CurrentAccount(102, 20000,"Ram");
        IAccount fd = new FixedDepositAccount(103,50000,"Priya");
        
        // Print info — each type prints differently (POLYMORPHISM)
        Console.WriteLine("--- Account Details ---");
        savings.PrintInfo();
        Console.WriteLine();
        current.PrintInfo();
        Console.WriteLine();
        fd.PrintInfo();

        // Savings — normal operations
        Console.WriteLine("\n--- Savings Account ---");
        savings.Deposit(5000);
        savings.Withdraw(3000);
        Console.WriteLine($"  Interest: ₹{savings.CalculateInterest()}");
        Console.WriteLine($"  Balance: ₹{savings.GetBalance()}");
        
        // Current — min balance check
        Console.WriteLine("\n--- Current Account ---");
        current.Withdraw(16000);  // ❌ would go below 5000
        current.Withdraw(10000);  // ✅ balance remains 10000 (> 5000)
        Console.WriteLine($"  Interest: ₹{current.CalculateInterest()}");
        Console.WriteLine($"  Balance: ₹{current.GetBalance()}");

        // FD — withdraw blocked
        Console.WriteLine("\n--- Fixed Deposit ---");
        fd.Deposit(10000);        // ✅ add more
        fd.Withdraw(5000);        // ❌ not allowed
        Console.WriteLine($"  Interest: ₹{fd.CalculateInterest()}");
        Console.WriteLine($"  Balance: ₹{fd.GetBalance()}");
        
        // Transfer — Savings to Current
        ((BankAccount)savings).TransferAmount((BankAccount)current, 2000);
        Console.WriteLine($"  Amit balance: ₹{savings.GetBalance()}");
        Console.WriteLine($"  Ram balance: ₹{current.GetBalance()}");


        // Shopping Cart with Payment Strategy
        Console.WriteLine("\n--- Shopping Cart Payment ---");
        ShoppingCart cart = new ShoppingCart();
        cart.AddItem("Laptop", 50000);
        cart.AddItem("Mouse", 1000);
        // cart.RemoveItem("Mouse");
        cart.Checkout(new CashPayment());

        // Notification Factory
        Console.WriteLine("\n--- Notification Factory ---");
        /*
            factory static kyun banate hain?" 
            Toh bolna: "Kyunki factory sirf ek utility/helper hai jiska kaam objects return karna hai. 
            Uska khud ka koi state (data) nahi hota, 
            isliye usko static rakhte hain taaki har baar uska naya object banane me memory waste na ho
        */
        // var factory = NotificationFactory.CreateNotification("Email", "Amit"); 
        INotification factory = NotificationFactory.CreateNotification("SMS", "Amit");
        factory.Notify();

        // Logger
        Logger logger = Logger.GetInstance();
        
        logger.SetLevel("WARN");
        
        logger.Info("Yeh print NAHI hoga");
        logger.Warn("Yeh print HOGA");
        logger.Error("Yeh bhi print HOGA");

        // Configuration Manager (Singleton Test)
        Console.WriteLine("\n--- Configuration Manager ---");
        
        // Instance 1 laate hain aur kuch settings daalte hain
        ConfigurationManager config1 = ConfigurationManager.GetInstance();
        config1.SetSetting("App_Theme", "Dark");
        config1.SetSetting("DB_Host", "localhost");
        
        Console.WriteLine($"Config1 - Theme is: {config1.GetSetting("App_Theme")}");

        // Instance 2 laate hain (Yahan naya object nahi banna chahiye, purana hi aana chahiye)
        ConfigurationManager config2 = ConfigurationManager.GetInstance();
        
        // Check karte hain kya config2 mein purani settings hain?
        Console.WriteLine($"Config2 - DB_Host is: {config2.GetSetting("DB_Host")}");
        Console.WriteLine($"Config2 - Unknown Setting is: {config2.GetSetting("API_Key")}");


        Console.WriteLine("-----------Observer Pattern-Notifiers------------");
        var sms = new SMSNotifier();
        var email = new EmailNotifier();
        var loggerObser = new TransactionLogger();

        ((BankAccount)savings).Subscribe(sms);
        ((BankAccount)savings).Subscribe(email);
        ((BankAccount)savings).Subscribe(loggerObser);

        Console.WriteLine("\n--- Savings Account ---");
        savings.Deposit(5000);
        savings.Withdraw(3000);
        Console.WriteLine($"  Interest: ₹{savings.CalculateInterest()}");
        
        Console.WriteLine("\n--- Loan Account ---");
        var loan = new LoanApplication(1,"Amit", 10000);
        loan.Review();
        loan.Reject();
        loan.Approve();
        loan.Disburse();

        // Decorator
        Console.WriteLine("\n--- Decorator Pattern ---");
        savings = new SMSAlertDecorator(savings);
        savings = new InsuranceDecorator(savings);
        savings = new LockerFacilityDecorator(savings);
        Console.WriteLine(savings.GetDescription());
        Console.WriteLine("Total Monthly Charge: " + savings.GetMonthlyCharge());
        Console.WriteLine("\n✅ === DONE ===");
        }
    }
}
