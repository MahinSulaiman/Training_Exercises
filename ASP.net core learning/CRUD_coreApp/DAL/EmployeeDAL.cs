using CRUD_coreApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CRUD_coreApp.DAL
{
    public class EmployeeDAL
    {
        SqlConnection connection = null;
        //SqlCommand command = null;
        //public  static IConfiguration Configuration { get; set; }
        //private string GetConnectionString()
        //{
        //    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory
        //        ()).AddJsonFile("appsettings.json");
        //    Configuration = builder.Build();
        //    return Configuration.GetConnectionString("DefaultConnection");
        //}

        private readonly IConfiguration _configuration;

        // Inject IConfiguration through the constructor
        public EmployeeDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }


        public List<Employee> GetEmployees()
        {
            List<Employee> listEmployee = new List<Employee>();

            using (var connection = CreateConnection())
            {
                //var command = connection.CreateCommand();
                var command = new SqlCommand("usp_GetEmployees", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_GetEmployees";

                connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    Employee employee = new Employee();
                    employee.EmployeeId = Convert.ToInt32(dr["employeeId"]);
                    employee.FirstName = dr["firstName"].ToString();
                    employee.LastName = dr["lastName"].ToString();
                    employee.DateOfBirth = Convert.ToDateTime(dr["dateOfBirth"]).Date;
                    employee.Email = dr["email"].ToString();
                    employee.Salary = Convert.ToDouble(dr["salary"]);

                    listEmployee.Add(employee);
                }
                connection.Close();
            }


                return listEmployee;
        }

        //Get employee dtails by Id
        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = new Employee();
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("usp_GetEmployeeById", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_GetEmployees";

                connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                   
                    employee.EmployeeId = Convert.ToInt32(dr["employeeId"]);
                    employee.FirstName = dr["firstName"].ToString();
                    employee.LastName = dr["lastName"].ToString();
                    employee.DateOfBirth = Convert.ToDateTime(dr["dateOfBirth"]).Date;
                    employee.Email = dr["email"].ToString();
                    employee.Salary = Convert.ToDouble(dr["salary"]);
                }
                connection.Close();
            }
            return employee;
        }

        //ADD employee
        public bool InsertEmployee(Employee employee)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("usp_InsertEmployee", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Salary", employee.Salary);

                connection.Open();
                result = command.ExecuteNonQuery();
                connection.Close();          
            }
            return result > 0;
        }

        //Update Employee
        public bool UpdateEmployee(Employee employee)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("usp_UpdateEmployee", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Salary", employee.Salary);

                connection.Open();
                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return result > 0;

        }

        //Delete Employee
        public bool DeleteEmployee(int employeeId)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("usp_DeleteEmployee", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                connection.Open();
                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return result > 0;
        }
    }
}
