using System;
namespace LldPractice.CSharp.Bank;

public class CurrentAccount : BankAccount
{
    private const int MinBalance = 5000;
    public CurrentAccount(int accountNo, double balance,string name):base(accountNo,balance,name){}

    public override double CalculateInterest()
    {
        // throw new NotImplementedException();
        return 0;   // current account — no interest
    }

    public override void Withdraw(double amount)
    {
        if(balance - amount < MinBalance)
        {
            // throw new InvalidOperationException($"Insufficient balance! Available: {MinBalance}");
            Console.WriteLine($"Insufficient balance! Available: {MinBalance}");
            return;
        }
        base.Withdraw(amount);
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"  Type: Savings | Interest Rate: {MinBalance}");
    }   
}
