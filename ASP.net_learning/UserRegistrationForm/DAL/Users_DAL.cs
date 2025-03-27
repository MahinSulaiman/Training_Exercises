using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using UserRegistrationForm.Models;

namespace UserRegistrationForm.DAL
{
	public class Users_DAL
	{
		private readonly SqlConnection connection;
		public Users_DAL()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["UsersConnectionString"].ToString();
			connection = new SqlConnection(connectionString);
		}

		//fetch all users
		public List<Users> GetAllUsers()
		{
			List<Users> userList = new List<Users>();

			using (SqlCommand command = new SqlCommand("usp_GetAllUsers", connection))
			{
				command.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlData = new SqlDataAdapter(command);
				DataTable dtUsers = new DataTable();

				connection.Open();
				sqlData.Fill(dtUsers);
				connection.Close();

                foreach (DataRow dr in dtUsers.Rows)
                {
					userList.Add(new Users
					{
						UserId = Convert.ToInt32(dr["userId"]),
						FirstName = dr["firstName"].ToString(),
						LastName = dr["lastname"].ToString(),
						DateOfBirth = Convert.ToDateTime(dr["dateOfBirth"]).Date,
						Gender = dr["gender"].ToString(),
						PhoneNumber = Convert.ToInt64(dr["phoneNumber"]),
						EmailAddress = dr["emailAddress"].ToString(),
						Address = dr["address"].ToString(),
						State = dr["state"].ToString(),
						City = dr["city"].ToString(),
						UserName = dr["userName"].ToString()
						
					});
                }
				return userList;
            }			
		}

        //create  user
		public bool InsertUser(Users user)
		{
			int result = 0;
			using(SqlCommand command=new SqlCommand("usp_InsertUser", connection))
			{
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@FirstName",user.FirstName);
                command.Parameters.AddWithValue("@LastName",user.LastName);
                command.Parameters.AddWithValue("@DateOfBirth",user.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", user.Gender);
                command.Parameters.AddWithValue("@PhoneNumber",user.PhoneNumber);
                command.Parameters.AddWithValue("@EmailAddress",user.EmailAddress);
                command.Parameters.AddWithValue("@Address",user.Address);
                command.Parameters.AddWithValue("@State",user.State);
                command.Parameters.AddWithValue("@City",user.City);
                command.Parameters.AddWithValue("@UserName",user.UserName);
                command.Parameters.AddWithValue("@Password",user.Password);

                connection.Open();
				result=command.ExecuteNonQuery();
				connection.Close();

				if (result > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
				
			}
		}

		//fetch user details by id
		public Users GetUserById(int id)
		{
			Users user = new Users();
			using (SqlCommand command = new SqlCommand("usp_GetUserById", connection))
			{
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@UserId", id);

                SqlDataAdapter sqlData = new SqlDataAdapter(command);
                DataTable dtUsers = new DataTable();

                connection.Open();
				sqlData.Fill(dtUsers);
				connection.Close();

                if (dtUsers.Rows.Count == 0)
                {
                    return null; 
                }

                foreach (DataRow dr in dtUsers.Rows)
                {
						user.UserId = Convert.ToInt32(dr["userId"]);
						user.FirstName = dr["firstName"].ToString();
						user.LastName = dr["lastname"].ToString();
						user.DateOfBirth = Convert.ToDateTime(dr["dateOfBirth"]).Date;
						user.Gender = dr["gender"].ToString();
						user.PhoneNumber = Convert.ToInt64(dr["phoneNumber"]);
						user.EmailAddress = dr["emailAddress"].ToString();
						user.Address = dr["address"].ToString();
						user.State = dr["state"].ToString();
						user.City = dr["city"].ToString();
						user.UserName = dr["userName"].ToString();
						user.Password = dr["password"].ToString();
                }
            }

                return user;
		}

		//update user details
		public bool UpdateUser(Users user)
		{
			int result = 0;
            using (SqlCommand command = new SqlCommand("usp_UpdateUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@UserId", user.UserId);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", user.Gender);
                command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                command.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                command.Parameters.AddWithValue("@Address", user.Address);
                command.Parameters.AddWithValue("@State", user.State);
                command.Parameters.AddWithValue("@City", user.City);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Password", user.Password);

                connection.Open();
                result = command.ExecuteNonQuery();
                connection.Close();

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

		//delete user
		public bool DeleteUser(int id)
		{
			int result = 0;
			using (SqlCommand command = new SqlCommand("usp_DeleteUser", connection))
			{
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@UserId", id);

                connection.Open();
                result = command.ExecuteNonQuery();
                connection.Close();

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            }
    }
}