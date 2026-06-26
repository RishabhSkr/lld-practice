# C# LLD Practice

Use this folder for C# OOP and design practice.

Suggested structure:
- Add one problem per file or folder.
- Focus on classes, interfaces, inheritance, composition, encapsulation, and SOLID principles.
- Keep notes for trade-offs and edge cases.
```

    LldPractice/
    ├── Bank/                          ← Task 1 (Interface + Polymorphism)
    │   ├── IAccount.cs                   → interface
    │   ├── BankAccount.cs                → abstract base class
    │   ├── SavingsAccount.cs             → concrete
    │   ├── CurrentAccount.cs             → concrete
    │   ├── FixedDepositAccount.cs        → concrete
    │   └── BankMain.cs                   → Main() demo
    │
    ├── ShoppingCart/                   ← Task 2 (Strategy)
    │   ├── IPaymentStrategy.cs           → interface
    │   ├── CreditCardPayment.cs          → concrete strategy
    │   ├── UPIPayment.cs                 → concrete strategy
    │   ├── ShoppingCart.cs               → cart class
    │   └── ShopMain.cs                   → Main()
    │
    ├── Notification/                  ← Task 3 (Factory)
    │   └── ...
    │
    ├── Logger/                        ← Task 4 (Singleton)
    │   └── ...
    │
    ├── StockMarket/                   ← Task 5 (Observer)
    │   └── ...
    │
    ├── OrderSystem/                   ← Task 6 (State)
    │   └── ...
    │
    └── CoffeeShop/                    ← Task 7 (Decorator)
        └── ...
```

Task 2: Strategy Pattern 💳
Pattern: Same kaam ke multiple tarike — runtime pe choose karo.

Problem:
E-commerce checkout mein payment:
- CreditCard  → "Credit Card ending 1234 charged ₹{amount}"
- UPI         → "UPI {upiId} charged ₹{amount}"
- NetBanking  → "Net Banking {bankName} charged ₹{amount}"
- Cash on Delivery → "COD order placed. Pay ₹{amount} on delivery"
User checkout ke time decide karta hai kaunsa payment use karna hai.

``` 
Task 3: Factory Pattern 🏭
Pattern: Object creation ek jagah centralize karo. Client ko concrete class pata nahi honi chahiye.

Problem:
Notification System — type ke basis pe notification bhejo:
- EmailNotification  → "Email sent to {email}: {message}"
- SMSNotification    → "SMS sent to {phone}: {message}"
- PushNotification   → "Push sent to {deviceId}: {message}"
User bole "email" → system automatically EmailNotification create kare.
Kya banana hai:

csharp

// Step 1: Abstract class ya Interface
abstract class Notification
{
    public abstract void Send(string message);
}
// Step 2: 3 concrete classes (Email, SMS, Push)
// Step 3: Factory class
static class NotificationFactory
{
    static Notification Create(string type, string target)
    {
        // type = "email" / "sms" / "push"
        // target = email address / phone / device id
        // return appropriate Notification object
    }
}
// Step 4: Main mein
var notif1 = NotificationFactory.Create("email", "amit@gmail.com");
notif1.Send("Your order has been shipped!");
var notif2 = NotificationFactory.Create("sms", "9876543210");
notif2.Send("OTP is 4521");
var notif3 = NotificationFactory.Create("push", "device-xyz");
notif3.Send("New message received");
Expected Output:


📧 Email sent to amit@gmail.com: Your order has been shipped!
📱 SMS sent to 9876543210: OTP is 4521
🔔 Push sent to device-xyz: New message received
Interviewer bolega: "WhatsApp notification add karna ho toh?"

Bolo: "Ek WhatsAppNotification class banaunga, Factory mein ek case add karunga. Existing code untouched."


Task 4: Singleton Pattern 🔒

Pattern: Poore system mein sirf EK instance hona chahiye.

Problem:


Logger system — poore application mein ek hi logger hona chahiye.
Multiple classes se log karo, sab SAME file/console mein jaaye.
Logger features:
- Log(message) → timestamp ke saath print karo
- SetLevel(level) → "INFO", "WARN", "ERROR"
- Sirf us level aur usse upar ke logs dikhao
  - INFO shows: INFO + WARN + ERROR
  - WARN shows: WARN + ERROR
  - ERROR shows: ERROR only
Kya banana hai:

csharp

// Step 1: Singleton Logger
class Logger
{
    private static Logger instance;
    private string level = "INFO";  // default
    private Logger() { }   // private constructor!
    public static Logger GetInstance() { /* singleton logic */ }
    public void SetLevel(string level) { /* set level */ }
    public void Info(string msg) { /* print if level allows */ }
    public void Warn(string msg) { /* print if level allows */ }
    public void Error(string msg) { /* print if level allows */ }
}
// Step 2: Main mein — verify it's SAME instance
var logger1 = Logger.GetInstance();
var logger2 = Logger.GetInstance();
Console.WriteLine(logger1 == logger2);  // TRUE — same object!
logger1.Info("App started");
logger1.Warn("Low memory");
logger1.Error("Crash!");
logger1.SetLevel("WARN");
logger1.Info("This should NOT print");
logger1.Warn("This SHOULD print");
logger1.Error("This SHOULD print");

```