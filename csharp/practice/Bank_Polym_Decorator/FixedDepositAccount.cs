using System;
namespace LldPractice.CSharp.Bank;

public class FixedDepositAccount : BankAccount
{
    private const double InterestRate = 0.07;
    public FixedDepositAccount(int accountNo, double balance,string name):base(accountNo,balance,name){}

    // deposit allowed
    public override void Withdraw(double amount)
    {
        // throw new NotImplementedException();
        Console.WriteLine("Withdrawal not allowed for Fixed Deposit Account!");
    }
    
    public override double CalculateInterest(){return balance*InterestRate;}

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"  Type: Savings | Interest Rate: {InterestRate * 100}%");
    }
}
