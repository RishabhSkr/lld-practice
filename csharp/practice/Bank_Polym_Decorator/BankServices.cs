/*
Pattern: Decorator — Bank Account Services
Banking context mein hi! Bank mein account ke saath extra services add hoti hain:



Base Account: SavingsAccount (Monthly charge: ₹0)
Add-on Services (Decorators):
- SMS Alert Service     → +₹25/month
- Insurance Cover       → +₹100/month  
- Premium Support       → +₹50/month
- Locker Facility       → +₹200/month
Customer koi bhi combination choose kar sakta hai!

*/

using LldPractice.CSharp.Bank;

public abstract class AccountDecorator : IAccount{
    protected IAccount _innerAccount;

    public AccountDecorator(IAccount account){
        _innerAccount = account;
    }

    public void Deposit(double amt) => _innerAccount.Deposit(amt);
    public void Withdraw(double amt) => _innerAccount.Withdraw(amt);
    
    public void TransferAmount(BankAccount toAccount, double amt) => _innerAccount.TransferAmount(toAccount, amt);
    
    public double GetBalance() => _innerAccount.GetBalance();
    
    public double CalculateInterest() => _innerAccount.CalculateInterest();
    
    public void PrintInfo() => _innerAccount.PrintInfo();
    
    public abstract double GetMonthlyCharge();
    
    public abstract string GetDescription();
}


public class SMSAlertDecorator : AccountDecorator
{
    public SMSAlertDecorator(IAccount account) : base(account){}

    public override double GetMonthlyCharge(){
        return _innerAccount.GetMonthlyCharge() + 25;
    }

    public override string GetDescription(){
        return _innerAccount.GetDescription() + ", SMS Alert";
    }
}

public class InsuranceDecorator : AccountDecorator{
    public InsuranceDecorator(IAccount account) : base(account){}

    public override double GetMonthlyCharge(){
        return _innerAccount.GetMonthlyCharge() + 100;
    }

    public override string GetDescription(){
        return _innerAccount.GetDescription() + ", Insurance";
    }
}

public class PremiumSupportDecorator : AccountDecorator{
    public PremiumSupportDecorator(IAccount account) : base(account){}

    public override double GetMonthlyCharge(){
        return _innerAccount.GetMonthlyCharge() + 50;
    }

    public override string GetDescription(){
        return _innerAccount.GetDescription() + ", Premium Support";
    }
}

public class LockerFacilityDecorator : AccountDecorator{
    public LockerFacilityDecorator(IAccount account) : base(account){}

    public override double GetMonthlyCharge(){
        return _innerAccount.GetMonthlyCharge() + 200;
    }

    public override string GetDescription(){
        return _innerAccount.GetDescription() + ", Locker Facility";
    }
}
