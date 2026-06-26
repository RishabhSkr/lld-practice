using System;
namespace LldPractice.CSharp.Bank;

public class SavingsAccount : BankAccount
{
    private const double InterestRate = 0.04;
    public SavingsAccount(int accountNo, double balance,string name):base(accountNo,balance,name){}

    public override double CalculateInterest(){return balance*InterestRate;}   

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"  Type: Savings | Interest Rate: {InterestRate * 100}%");
    }
}


