using System;
using System.Collections.Generic;
using LldPractice.CSharp.AutoNotifier_Observer;
namespace LldPractice.CSharp.Bank;

// exception handling
/**
        IAccount (interface — contract)
            ↑ implements
        BankAccount (abstract class — common code)
            ↑ extends
    ┌───────┼──────────┐
Savings  Current   FixedDeposit

    abstract class    → "Kuch implement karo, kuch child pe chhodo"
    interface         → "Sirf contract, koi code nahi"
    virtual           → "Default hai, child CHANGE KAR SAKTA hai"
    abstract method   → "Default NAHI hai, child MUST implement"
    override          → "Parent ka behavior change kar raha hoon"
    base.Method()     → "Parent ka original code bhi chala lo"
    protected         → "Sirf class + children access karein"

**/ 

 public abstract class BankAccount:IAccount
{
        public int accountNo {get; set;}
        public string ownerName{get; set;}
        public double balance {get; private set;}
        private List<ITransactionObserver> observer = new List<ITransactionObserver>();

        protected BankAccount(int accountNo, double balance,string name)
        {
            this.accountNo = accountNo;
            this.balance = balance;
            ownerName=name;
        }

        // methods
        public void Subscribe(ITransactionObserver observer){
            this.observer.Add(observer);

        }

        public void Unsubscribe(ITransactionObserver observer){
            this.observer.Remove(observer);
        }

        public void NotifyObservers(NotifierType type, double amount)
        {
            foreach (var o in observer)
            {
                o.OnTransaction(ownerName,type,amount,balance);
            }
        }

        public virtual void  Deposit(double amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount} to account {accountNo}");

            NotifyObservers(NotifierType.DEPOSIT, amount);
        }
        
        public virtual void Withdraw(double amount)
        {
            if (amount <= 0)
            throw new ArgumentException("Amount must be positive!");

            if (amount > balance)
            throw new InvalidOperationException($"Insufficient balance! Available: {balance}");

            balance -= amount;
            Console.WriteLine($"Withdrawn {amount} from {ownerName}'s account");

            NotifyObservers(NotifierType.WITHDRAW, amount);
        }


        public virtual void TransferAmount(BankAccount toAccount,double amt)
        {   
            try
            {
                Withdraw(amt);
                toAccount.Deposit(amt);
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public double GetBalance()=> balance;

        // ABSTRACT METHOD — each child MUST implement
        public abstract double CalculateInterest();

        // VIRTUAL METHOD — child CAN override (optional)
        public virtual void PrintInfo()
        {
            Console.WriteLine($"  Account: {accountNo} | Owner: {ownerName} | Balance: ₹{balance}");
        }

        //Decorator functions
        
        public virtual double GetMonthlyCharge()=>0;
        public virtual string GetDescription()=>$"Saving Account [{ownerName}]";
}