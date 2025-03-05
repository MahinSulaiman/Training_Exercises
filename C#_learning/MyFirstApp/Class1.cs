using System.Diagnostics;
using System.Xml.Linq;

namespace MyFirstApp
{
    class ShoppingCart
    {
        List<Product> cartItems = new List<Product>();
        public void AddProduct(Product p)
        {
            cartItems.Add(p);
            Console.WriteLine($"{p.Name} added to shopping cart");
        }

        public void RemoveProduct(int id)
        {
            Product pdt = cartItems.Find(p => p.Id == id)!;
            if (pdt != null)
            {
                cartItems.Remove(pdt);
            }
            else
            {
                Console.WriteLine("product not found");
            }
        }

        public void DisplayCart()
        {
            foreach(Product item in cartItems)
            {
                Console.WriteLine($"Id:{item.Id}\nName:{item.Name}\nPrice:{item.Price}\nQuantity:{item.Quantity}");
            }
        }

        public double CalculateTotal()
        {
            double totalPrice = 0;
            foreach(Product item in cartItems)
            {
                totalPrice += item.Price;
            }
            return totalPrice;
        }
    }
}
