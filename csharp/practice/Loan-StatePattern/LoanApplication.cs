/*
States:   Applied → UnderReview → Approved → Disbursed
                         ↘ Rejected (from UnderReview only)
Rules:
- Applied      → can move to UnderReview
- UnderReview  → can Approve OR Reject
- Approved     → can Disburse
- Rejected     → NOTHING (final state)
- Disbursed    → NOTHING (final state)
*/
using System;
namespace LldPractice.CSharp.Loan_StatePattern;

public enum LoanState
{
    Applied,
    UnderReview,
    Approved,
    Rejected,
    Disbursed

}

public class LoanApplication
{
    public int LoanId {get;}
    public string ApplicantName {get;}

    public double Amount {get;}

    private LoanState State {get; set;}

    public LoanApplication(int loanId, string applicantName, double amount)
    {
        this.LoanId = loanId;
        this.ApplicantName = applicantName;
        this.Amount = amount;
        this.State = LoanState.Applied;
        Console.WriteLine("Loan applied.");
    }
    public void Review()
    {   
        if (State == LoanState.Applied)
        {
            Console.WriteLine("Loan under review.");
            State = LoanState.UnderReview;
            return;
        }
       Console.WriteLine("Loan is not under review.");
        
    }
    public void Approve()
    {
        if (State == LoanState.UnderReview)
        {
            State = LoanState.Approved;
            Console.WriteLine("Loan approved.");
            return;
        }
        Console.WriteLine("1. Loan should be under review.");

    }
    public void Reject()
    {
        if (State == LoanState.UnderReview)
        {
            State = LoanState.Rejected;
            Console.WriteLine("Loan rejected.");
            return;
        }
        Console.WriteLine("2. Loan should be under review.");
    }
    public void Disburse()
    {
        if (State == LoanState.Approved)
        {
            State = LoanState.Disbursed;
            Console.WriteLine("Loan disbursed.");
            return;
        }
        Console.WriteLine("Loan should be approved.");
    }

}