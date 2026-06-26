namespace LldPractice.CSharp.Bank;

public interface IAccount
{
    void Deposit(double amount);
    void Withdraw(double amount);
    void TransferAmount(BankAccount toAccount,double amt);
    double GetBalance();
    double CalculateInterest();
    void PrintInfo();
    double GetMonthlyCharge();
    string GetDescription();
}