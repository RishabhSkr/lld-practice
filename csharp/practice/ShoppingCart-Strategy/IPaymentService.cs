namespace LldPractice.CSharp.ShoppingCart_Strategy;

// Step 1: Strategy Interface
public interface IPaymentStrategy
{
    void Pay(double amount);
}
