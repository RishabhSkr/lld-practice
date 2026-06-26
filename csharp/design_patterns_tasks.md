# 🎯 Design Patterns — Task-Based Learning (C#)

> **Rule:** Pehle KHUD try karo (30 min). Phir mujhe bhejo. Main review karunga.
> Har task ek pattern sikhayega. 7 tasks = 7 patterns = interview ready.

---

## Roadmap:

```
Task 1: Interface + Polymorphism  → Bank Accounts        ⬜ (Day 3)
Task 2: Strategy Pattern          → Payment System        ⬜ (Day 4)
Task 3: Factory Pattern           → Notification System   ⬜ (Day 5)
Task 4: Singleton Pattern         → Logger                ⬜ (Day 6)
Task 5: Observer Pattern          → Stock Market Alert     ⬜ (Day 7)
Task 6: State Pattern             → Order Lifecycle        ⬜ (Day 8)
Task 7: Decorator Pattern         → Coffee Shop            ⬜ (Day 9)
```

---

## Task 1: Interface + Polymorphism 🏦

**Pattern:** Interface — ek contract define karo, multiple classes implement karein.

**Problem:**
```
Bank mein 3 type ke accounts hain:

1. SavingsAccount
   - Interest rate: 4%
   - Normal deposit/withdraw

2. CurrentAccount
   - Interest rate: 0%
   - Minimum balance: 5000 (isse neeche withdraw reject)

3. FixedDeposit
   - Interest rate: 7%
   - Withdraw NAHI kar sakte (throw error)
```

**Kya banana hai:**
```csharp
// Step 1: Interface banao
interface IAccount
{
    void Deposit(double amount);
    void Withdraw(double amount);
    double GetBalance();
    double CalculateInterest();  // annual interest return karo
}

// Step 2: 3 classes banao
class SavingsAccount : IAccount { /* implement karo */ }
class CurrentAccount : IAccount { /* implement karo */ }
class FixedDeposit : IAccount { /* implement karo */ }

// Step 3: Main mein
IAccount savings = new SavingsAccount("Amit", 10000);
IAccount current = new CurrentAccount("Ram", 20000);
IAccount fd = new FixedDeposit("Priya", 50000);

savings.Deposit(5000);
savings.Withdraw(3000);
Console.WriteLine($"Savings Interest: {savings.CalculateInterest()}");

current.Withdraw(16000);  // should FAIL (balance < 5000)

fd.Withdraw(1000);        // should FAIL (FD mein withdraw nahi)
Console.WriteLine($"FD Interest: {fd.CalculateInterest()}");
```

**Expected Output:**
```
Deposited 5000 to Amit's Savings Account
Withdrawn 3000 from Amit's Savings Account
Savings Interest: 480    (12000 × 4%)

Cannot withdraw! Balance would fall below minimum 5000
FD Withdrawal not allowed!
FD Interest: 3500        (50000 × 7%)
```

**Interviewer bolega:** "Interface kyu use kiya? Abstract class se kya fark hai?"
> Bolo: "Interface = pure contract (kya karna hai). Abstract class = partial implementation (kuch code share karna hai). Yahan sab accounts independently implement karte hain, shared code nahi hai, toh interface sahi hai."

---

## Task 2: Strategy Pattern 💳

**Pattern:** Same kaam ke multiple tarike — runtime pe choose karo.

**Problem:**
```
E-commerce checkout mein payment:
- CreditCard  → "Credit Card ending 1234 charged ₹{amount}"
- UPI         → "UPI {upiId} charged ₹{amount}"
- NetBanking  → "Net Banking {bankName} charged ₹{amount}"
- Cash on Delivery → "COD order placed. Pay ₹{amount} on delivery"

User checkout ke time decide karta hai kaunsa payment use karna hai.
```

**Kya banana hai:**
```csharp
// Step 1: Strategy Interface
interface IPaymentStrategy
{
    void Pay(double amount);
}

// Step 2: 4 concrete strategies banao (CreditCard, UPI, NetBanking, COD)

// Step 3: ShoppingCart class banao
class ShoppingCart
{
    List<(string item, double price)> items = new();

    void AddItem(string item, double price);
    double GetTotal();
    void Checkout(IPaymentStrategy paymentMethod);  // strategy inject hoga
}

// Step 4: Main mein
var cart = new ShoppingCart();
cart.AddItem("Shoes", 2000);
cart.AddItem("T-Shirt", 800);

// Same cart, different payment strategies
cart.Checkout(new UPI("rishabh@upi"));
// ya
cart.Checkout(new CreditCard("1234-5678-9012-3456"));
// ya
cart.Checkout(new COD());
```

**Expected Output:**
```
Cart Total: ₹2800
UPI rishabh@upi charged ₹2800
Payment successful!
```

**Interviewer bolega:** "Naya payment method add karna ho (PayPal) toh?"
> Bolo: "Sirf ek nayi class `PayPal : IPaymentStrategy` banaunga. Existing code mein koi change nahi. Open/Closed Principle."

---

## Task 3: Factory Pattern 🏭

**Pattern:** Object creation ek jagah centralize karo. Client ko concrete class pata nahi honi chahiye.

**Problem:**
```
Notification System — type ke basis pe notification bhejo:
- EmailNotification  → "Email sent to {email}: {message}"
- SMSNotification    → "SMS sent to {phone}: {message}"
- PushNotification   → "Push sent to {deviceId}: {message}"

User bole "email" → system automatically EmailNotification create kare.
```

**Kya banana hai:**
```csharp
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
```

**Expected Output:**
```
📧 Email sent to amit@gmail.com: Your order has been shipped!
📱 SMS sent to 9876543210: OTP is 4521
🔔 Push sent to device-xyz: New message received
```

**Interviewer bolega:** "WhatsApp notification add karna ho toh?"
> Bolo: "Ek `WhatsAppNotification` class banaunga, Factory mein ek case add karunga. Existing code untouched."

---

## Task 4: Singleton Pattern 🔒

**Pattern:** Poore system mein sirf EK instance hona chahiye.

**Problem:**
```
Logger system — poore application mein ek hi logger hona chahiye.
Multiple classes se log karo, sab SAME file/console mein jaaye.

Logger features:
- Log(message) → timestamp ke saath print karo
- SetLevel(level) → "INFO", "WARN", "ERROR"
- Sirf us level aur usse upar ke logs dikhao
  - INFO shows: INFO + WARN + ERROR
  - WARN shows: WARN + ERROR
  - ERROR shows: ERROR only
```

**Kya banana hai:**
```csharp
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

**Expected Output:**
```
True
[INFO]  2025-06-22 12:00:00 — App started
[WARN]  2025-06-22 12:00:01 — Low memory
[ERROR] 2025-06-22 12:00:02 — Crash!
[WARN]  2025-06-22 12:00:03 — This SHOULD print
[ERROR] 2025-06-22 12:00:04 — This SHOULD print
```

**Interviewer bolega:** "Thread-safe kaise karoge?"
> Bolo: "`lock` use karunga `GetInstance` mein — double-checked locking pattern."

---

## Task 5: Observer Pattern 👀

**Pattern:** Jab koi state change ho, automatically sabko notify karo.

**Problem:**
```
Stock Market Alert System:
- Stocks hain: TCS, Infosys, Reliance
- Users subscribe karte hain specific stocks pe
- Jab stock price change ho → subscribed users ko alert jaaye
- User unsubscribe bhi kar sake
```

**Kya banana hai:**
```csharp
// Step 1: Observer Interface
interface IInvestor
{
    void Update(string stockName, double price);
}

// Step 2: Concrete Investors
class Investor : IInvestor { /* name, update method */ }

// Step 3: Stock class (Subject)
class Stock
{
    string name;
    double price;
    List<IInvestor> subscribers;

    void Subscribe(IInvestor investor);
    void Unsubscribe(IInvestor investor);
    void SetPrice(double newPrice);  // price change → notify all
}

// Step 4: Main
var tcs = new Stock("TCS", 3500);
var amit = new Investor("Amit");
var priya = new Investor("Priya");

tcs.Subscribe(amit);
tcs.Subscribe(priya);

tcs.SetPrice(3600);   // Both get notified
tcs.Unsubscribe(amit);
tcs.SetPrice(3550);   // Only Priya gets notified
```

**Expected Output:**
```
[ALERT] Amit: TCS price changed to 3600
[ALERT] Priya: TCS price changed to 3600
Amit unsubscribed from TCS
[ALERT] Priya: TCS price changed to 3550
```

**Interviewer bolega:** "Real life mein kahan use hota hai?"
> Bolo: "Event systems, pub/sub, WebSocket notifications — PulseChat mein Socket.io ka `on`/`emit` essentially Observer pattern hai."

---

## Task 6: State Pattern 📦

**Pattern:** Object ka behavior state ke basis pe change ho. Invalid transitions block ho.

**Problem:**
```
E-commerce Order Lifecycle:

States:    Placed → Paid → Shipped → Delivered
                ↘ Cancelled (from Placed or Paid only)

Rules:
- Placed → can Pay or Cancel
- Paid → can Ship or Cancel (with refund message)
- Shipped → can Deliver only (cancel nahi)
- Delivered → nothing (final state)
- Cancelled → nothing (final state)
```

**Kya banana hai:**
```csharp
// Step 1: Order class with status
class Order
{
    public int OrderId { get; }
    public string Status { get; private set; } = "Placed";

    void Pay();      // Placed → Paid
    void Ship();     // Paid → Shipped
    void Deliver();  // Shipped → Delivered
    void Cancel();   // Placed/Paid → Cancelled
}

// Step 2: Each method checks current state, transitions or throws error

// Step 3: Main
var order = new Order(1);
order.Pay();       // Placed → Paid ✅
order.Ship();      // Paid → Shipped ✅
order.Cancel();    // ❌ ERROR: Cannot cancel shipped order!
order.Deliver();   // Shipped → Delivered ✅
order.Pay();       // ❌ ERROR: Order already delivered!
```

**Expected Output:**
```
Order #1: Placed → Paid
Order #1: Paid → Shipped
ERROR: Cannot cancel a Shipped order!
Order #1: Shipped → Delivered
ERROR: Order already Delivered, no actions allowed!
```

**Interviewer bolega:** "NexusERP mein toh Saga use kiya tha — yeh usse kaise different hai?"
> Bolo: "State Pattern single object ka lifecycle manage karta hai. Saga Pattern multiple services ke across distributed state transitions manage karta hai with compensation (rollback). Saga = State Pattern at distributed level."

---

## Task 7: Decorator Pattern ☕

**Pattern:** Object ko runtime pe extra features add karo, original class change kiye bina.

**Problem:**
```
Coffee Shop:
- Base coffees: Espresso (₹100), Latte (₹150)
- Add-ons (decorators): Milk (+₹20), Sugar (+₹10), Whipped Cream (+₹30)
- Customer koi bhi combination order kar sake
- Total price = base + all add-ons
```

**Kya banana hai:**
```csharp
// Step 1: Interface
interface ICoffee
{
    double GetCost();
    string GetDescription();
}

// Step 2: Base coffees
class Espresso : ICoffee { /* cost=100, desc="Espresso" */ }
class Latte : ICoffee { /* cost=150, desc="Latte" */ }

// Step 3: Decorators (wrap an ICoffee, add cost + description)
class MilkDecorator : ICoffee
{
    ICoffee coffee;  // wraps another coffee
    // GetCost() → coffee.GetCost() + 20
    // GetDescription() → coffee.GetDescription() + " + Milk"
}

// Step 4: Main — stack decorators!
ICoffee order = new Espresso();                    // ₹100
order = new MilkDecorator(order);                   // ₹120
order = new SugarDecorator(order);                  // ₹130
order = new WhippedCreamDecorator(order);            // ₹160

Console.WriteLine(order.GetDescription());  // Espresso + Milk + Sugar + Whipped Cream
Console.WriteLine(order.GetCost());         // 160
```

**Expected Output:**
```
Espresso + Milk + Sugar + Whipped Cream
Total: ₹160
```

**Interviewer bolega:** "Inheritance se kyu nahi kiya? MilkEspresso, SugarEspresso class bana lete"
> Bolo: "Combinations explode ho jaate — MilkEspresso, SugarEspresso, MilkSugarEspresso, MilkSugarCreamEspresso... Decorator se runtime pe koi bhi combination ban jaata hai. 3 base + 3 add-ons = infinite combinations, sirf 6 classes se."

---

## 📊 Pattern Summary

| # | Pattern | One Line | Kab use karo |
|---|---------|----------|-------------|
| 1 | **Interface** | Contract define karo | Multiple implementations chahiye |
| 2 | **Strategy** | Algorithm swap karo runtime pe | Payment, pricing, sorting |
| 3 | **Factory** | Object creation centralize karo | Type based pe object banana ho |
| 4 | **Singleton** | Ek hi instance globally | Logger, DB connection, config |
| 5 | **Observer** | State change pe notify karo | Notifications, events, pub/sub |
| 6 | **State** | Behavior state pe depend kare | Order lifecycle, booking status |
| 7 | **Decorator** | Extra features wrap karo | Add-ons, filters, middleware |

---

> **Rule: Daily 1 task. Pehle khud try karo. Phir mujhse check karwao.**
> **7 din = 7 patterns = LLD interview ready.** 💪
