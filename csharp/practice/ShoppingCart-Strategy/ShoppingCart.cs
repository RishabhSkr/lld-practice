using System.Collections.Generic;
using System.Linq;
namespace LldPractice.CSharp.ShoppingCart_Strategy;


public class ShoppingCart
{
    List<(string item, double price)> items = new();
    public void AddItem(string item, double price) => items.Add((item, price));
    public void RemoveItem(string item) => items.Remove(items.Find(x => x.item == item));
    // double GetTotal() => items.Sum(x => x.price);
    // double GetTotal()
    // {
    //     double total = 0;
    //     foreach (var item in items)
    //     {
    //         total += item.price;
    //     }
    //     return total;
    // }
    public double GetTotal() => items.Sum(x => x.price);
    public void Checkout(IPaymentStrategy paymentStrategy) => paymentStrategy.Pay(GetTotal());

}
