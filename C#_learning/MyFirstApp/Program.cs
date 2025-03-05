using MyFirstApp;

internal class Program
{
    static List<Product> items = new List<Product> {
                                new Product(1,"Mobile",50059.56,2),
                                new Product(2,"Book",125.00,5),
                                new Product(3,"Pen",25.00,15),
                                        };
    static ShoppingCart cart = new ShoppingCart();
    static void ViewProducts()
    {
        Console.WriteLine("Available Products");
        foreach(Product item in items)
        {
            item.DisplayProduct();
        }
    }

    static void AddToCart()
    {
        Console.WriteLine("enter product id");
        int id = Convert.ToInt32(Console.ReadLine());
        Product product = items.Find(p => p.Id == id)!;
        if (product != null)
        {
            cart.AddProduct(product);
        }
        else
        {
            Console.WriteLine("Product not found!");
        }
    }

    static void RemoveFromCart()
    {
        Console.WriteLine("enter product id");
        int id = Convert.ToInt32(Console.ReadLine());
        cart.RemoveProduct(id);

    }

    static void Checkout()
    {
        double amount = cart.CalculateTotal();
        Console.WriteLine($"Your total is: {amount}");
        Console.WriteLine("Thank you for your purchase!");
    }
    private static void Main(string[] args)
    {
        int choice;
        do
        {
            Console.WriteLine("E-Commerce Console Application");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. Add Product to Cart");
            Console.WriteLine("3. View Cart");
            Console.WriteLine("4. Remove Product from Cart");
            Console.WriteLine("5. Checkout");
            Console.WriteLine("6. Exit");
            Console.Write("Please enter your choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ViewProducts();
                    break;
                case 2:
                    AddToCart();
                    break;
                case 3:
                    cart.DisplayCart();
                    break;
                case 4:
                    RemoveFromCart();
                    break;
                case 5:
                    Checkout();
                    break;
                default:
                    Console.WriteLine("Invalid choice! Please select again.");
                    break;
            }

        } while (choice != 6);

    }
    
}