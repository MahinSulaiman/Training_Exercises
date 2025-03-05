// int age=35;
// string name="Appu";
// bool isActive=true;
// double dec=9.95;
// Console.Write("my name is "+name+" age:"+ age+" "+isActive+" "+dec);
// Console.Write("\n"+(int)dec);

// Console.WriteLine("enter a number");
// int num=Convert.ToInt32(Console.ReadLine());
// Console.WriteLine("squareroot of the number is"+Math.Sqrt(num));

//string
// string greeting="Hello World";
// Console.WriteLine(greeting.Length);
// Console.WriteLine($"first program is {greeting}");

// Array
// string[] cars={"BMW","ford","Benz","swift"};
// foreach (string item in cars)
// {
//     Console.WriteLine(item);
// }

// 2D array

// int[,] nums={{1,2,3},{8,7,6},{5,4,0}};
// foreach(int i in nums){
//     Console.WriteLine(i);
// }

//Exception-handling
// void Handle_Except(int a,int b){
//     try{
//         // int c=a/b;
//         Console.WriteLine(a/b);
//     }
//     catch(Exception e){
//         Console.WriteLine(e.Message);
//     }
// }


// Console.WriteLine("enter 2 numbers");
// int a=Convert.ToInt32(Console.ReadLine());
// int b=Convert.ToInt32(Console.ReadLine());
// Handle_Except(a,b);


//number guessing
// Random random=new Random();
// int target=random.Next(1,101);

// while(true){
//     Console.WriteLine("Guess a number between 1 and 100");
//     int num=Convert.ToInt32(Console.ReadLine());

//     if(num>target){
//         Console.WriteLine("Too high");
//     }
//     else if (num<target){
//         Console.WriteLine("Too low");
//     }
//     else{
//         Console.WriteLine("correct guess!");
//         break;
//     }
// }


//Exception-handling
void Handle_Except(int a,int b){
    try{
        // int c=a/b;
        Console.WriteLine(a/b);
    }
    catch(Exception e){
        Console.WriteLine(e.Message);
    }
}


Console.WriteLine("enter 2 numbers");
int a=Convert.ToInt32(Console.ReadLine());
int b=Convert.ToInt32(Console.ReadLine());
Handle_Except(a,b);
