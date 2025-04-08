using ClaysysLearningPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.AspNetCore.Identity;

namespace ClaysysLearningPortal.DAL
{
    public class UserDAL
    {
        private readonly IConfiguration _configuration;

        public UserDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// user registration
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(Users user)
        {
            int result = 0;
            var passswordHasher = new PasswordHasher<Users>();
            var hashedPassword = passswordHasher.HashPassword(null, user.Password);
            using (var connection = CreateConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("usp_AddUser", (SqlConnection)connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Gender", user.Gender);
                    command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                    command.Parameters.AddWithValue("@UserName", user.UserName);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                    command.Parameters.AddWithValue("@Role", user.Role);

                    connection.Open();
                    result = command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
               
            }
            return result > 0;
        }

        /// <summary>
        /// user login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserLogin Login(string username, string password)
        {           
            //string role = null;
            UserLogin loginUser = new UserLogin();
            using (var connection = CreateConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("usp_ValidateUser", (SqlConnection)connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", username);

                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string storedHashedPassword = dr["password"].ToString();
                            string storedRole = dr["role"].ToString();
                            loginUser.UserId = Convert.ToInt32(dr["userId"]);
                            loginUser.UserName = username;

                            var passwordHasher = new PasswordHasher<Users>();
                            var verificationResult = passwordHasher.VerifyHashedPassword(null, storedHashedPassword, password);

                            if (verificationResult == PasswordVerificationResult.Success)
                            {
                                loginUser.Role = storedRole;
                            }
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }              
            }
            return loginUser;
        }

        /// <summary>
        /// get all users
        /// </summary>
        /// <returns></returns>
        public List<Users> GetAllUsers()
        {
            List<Users> userList = new List<Users>();

            using (var connection = CreateConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("usp_GetAllUsers", (SqlConnection)connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Users user = new Users();
                        user.UserId = Convert.ToInt32(dr["userId"]);
                        user.FirstName = dr["firstName"].ToString();
                        user.LastName = dr["lastName"].ToString();
                        user.Email = dr["email"].ToString();
                        user.Gender = dr["gender"].ToString();
                        user.DateOfBirth = Convert.ToDateTime(dr["dateOfBirth"]);
                        user.UserName = dr["userName"].ToString();
                        user.Role = dr["role"].ToString();

                        userList.Add(user);
                    }
                }
                finally
                {
                    connection.Close();
                }    
            }
            return userList;
        }

        /// <summary>
        /// delete a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUser(int userId)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("usp_DeleteUser", (SqlConnection)connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    result = command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
            return result > 0;
        }

        /// <summary>
        /// update user details
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(Users user)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("usp_UpdateUser", (SqlConnection)connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Gender", user.Gender);
                    command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                    command.Parameters.AddWithValue("@UserName", user.UserName);
                    command.Parameters.AddWithValue("@Role", user.Role);

                    connection.Open();
                    result = command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
                
            }
            return result > 0;
        }

        /// <summary>
        /// get user details by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Users GetUserById(int userId)
        {
            Users user = new Users();
            using (var connection = CreateConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("GetUserById", (SqlConnection)connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        user.UserId = Convert.ToInt32(dr["userId"]);
                        user.FirstName = dr["firstName"].ToString();
                        user.LastName = dr["lastName"].ToString();
                        user.Email = dr["email"].ToString();
                        user.Gender = dr["gender"].ToString();
                        user.DateOfBirth = Convert.ToDateTime(dr["dateOfBirth"]);
                        user.UserName = dr["userName"].ToString();
                        user.Role = dr["role"].ToString();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return user;
        }

    }
}
