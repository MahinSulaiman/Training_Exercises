//internal class Program
//{
//    private static void Main(string[] args)
//    {
//        Console.WriteLine("Hello, World!");
//        Stack<int> num = new Stack<int>();
//        num.Push(5);
//        num.Push(8);
//        num.Push(9);
//        num.Peek();

//        Console.WriteLine("strings");
//        Stack<string> str = new Stack<string>();
//        str.Push("hello");
//        str.Push("hai");
//        str.Peek();

//    }
//}

//class Stack<T>
//{
//    private List<T> elements = new List<T>();
//    public void Push(T item)
//    {
//        elements.Add(item);
//        Console.WriteLine($"{item} inserted");
//    }

//    public  T Pop()
//    {
//        T top = elements[elements.Count - 1];
//        elements.RemoveAt(elements.Count-1);
//        Console.WriteLine($"{top} popped");
//        return top; 
//    }

//    public T Peek()
//    {
//        T top = elements[elements.Count - 1];
//        Console.WriteLine($"{top} is the top elment");
//        return top;
//    }
//};

//Delegates
//public delegate int Operation(int x, int y);

//public class Calculator
//{
//    public int Add(int x, int y) => x + y;
//    public int Multiply(int x, int y) => x * y;
//    public int Divide(int x, int y) => x/y;
//}

//internal class Program
//{
//    static void Main(string[] args)
//    {
//        Calculator calc = new Calculator();


//        Operation opAdd = new Operation(calc.Add);
//        Operation opMultiply = new Operation(calc.Multiply);
//        Operation opDivide = new Operation(calc.Divide);


//        Console.WriteLine(opAdd(5, 3));
//        Console.WriteLine(opMultiply(5, 3));
//        Console.WriteLine(opDivide(4,2));

//    }
//}


//events

using System;
using System.Timers;
public class Clock
{
    public event EventHandler Tick;
    private System.Timers.Timer timer;

    public Clock()
    {
        timer = new System.Timers.Timer(1000);
        timer.Elapsed += OnTimerElapsed;
    }

    public void Start()
    {
        timer.Start();
    }

    public void Stop()
    {
        timer.Stop();
    }


    protected virtual void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        Tick?.Invoke(this, EventArgs.Empty);
    }
}

public class Display
{
    public void Subscribe(Clock clock)
    {
        clock.Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        
        //Console.Clear();
        Console.WriteLine("Current Time: " + DateTime.Now.ToString("HH:mm:ss"));
    }
}

class Program
{
    static void Main(string[] args)
    {
        Clock clock = new Clock();
        Display display = new Display();

        display.Subscribe(clock);
        clock.Start();

        Console.WriteLine("Press Enter to stop the clock...");
        Console.ReadLine();

        clock.Stop();
    }
}