
using Microsoft.Data.SqlClient;



internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        SqlConnection conn = new SqlConnection("server=DESKTOP-KHDKEB7\\SQLEXPRESS;database=claysysTraining;integrated security=true");

        conn.Open();
        Console.WriteLine(conn.State);

        SqlCommand cmd = new SqlCommand("select * from Employee", conn);
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(reader["FirstName"].ToString()); 
        }

        // Close the reader
        reader.Close();


        conn.Close();
        Console.WriteLine(conn.State);





    }
}