using System.Data;
using ClaysysLearningPortal.Models;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;

namespace ClaysysLearningPortal.DAL
{
    public class CoursesDAL
    {

        //SqlConnection connection = null;
        private readonly IConfiguration _configuration;

        // Inject IConfiguration through the constructor
        public CoursesDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        //Get all courses
        public List<Courses> GetAllCourses()
        {
            List<Courses> courseList = new List<Courses>();
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("usp_GetAllCourses", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    Courses course = new Courses();
                    course.CourseId = dr.GetGuid("courseId");
                    course.Title = dr.GetString("title");
                    course.Description = dr.GetString("description");
                    course.Category = dr.GetString("categoryName");

                    courseList.Add(course);
                }
                connection.Close();
            }
            return courseList;
        }

        //fetch list of courses by category
        public List<Courses> GetCoursesByCategory(Guid? categoryId)
        {
            List<Courses> courseList = new List<Courses>();

            try
            {
                using (var connection = CreateConnection())
                {
                    var command = new SqlCommand("usp_GetCourseByCategory", (SqlConnection)connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CategoryId", categoryId);

                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Courses course = new Courses();
                            course.CourseId = dr.GetGuid(0);
                            course.Title = dr.GetString(1);
                            course.CourseImage = dr["courseImage"] as byte[];
                            Console.WriteLine(course.CourseId);

                            courseList.Add(course);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
            return courseList;
        }

        //to add courses
        public bool AddCourse(Courses course)
        {
            int result = 0;
            
            using (var memorystream=new MemoryStream())
            {

                using (var connection = CreateConnection())
                {
                    SqlCommand command = new SqlCommand("usp_InsertCourse", (SqlConnection)connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Title", course.Title);
                    command.Parameters.AddWithValue("@CourseImage", course.CourseImage);
                    command.Parameters.AddWithValue("@Description", course.Description);
                    command.Parameters.AddWithValue("@CourseFile", course.CourseFile);
                    command.Parameters.AddWithValue("@CategoryId", course.CategoryId);

                    connection.Open();
                    result = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return result > 0 ? true : false;
        }

        //get list of categories
        public List<Categories> GetCategories()
        {
            List<Categories> categories = new List<Categories>();
            using (var connection = CreateConnection())
            {
                string query = "SELECT * FROM Category";
                var command = new SqlCommand(query, (SqlConnection)connection);

                connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    Categories category = new Categories();
                    category.CategoryId = dr.GetGuid(0);
                    category.Category = dr.GetString(1);

                    categories.Add(category);
                }
                connection.Close();
            }
            return categories;
        }

        //get course details by courseId
        public Courses GetCourseDetails(int userId,Guid courseId)
        {
            Courses course = new Courses();
            using (var connection = CreateConnection())
            {
                SqlCommand command = new SqlCommand("usp_GetCourseDetailsById", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CourseId", courseId);

                SqlCommand cmd = new SqlCommand("usp_GetEnrollStatus", (SqlConnection)connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@courseId", courseId);

                connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    course.CourseId = dr.GetGuid("courseId");
                    course.Title = dr["title"].ToString();
                    course.CourseImage = dr["courseImage"] as byte[];
                    course.Description = dr["description"].ToString();
                    course.CourseFile = dr["courseFile"].ToString();
                }
                dr.Close();

                var result = cmd.ExecuteScalar();
                if (result!=DBNull.Value)
                {
                    course.EnrollStatus = (string)result;
                }
                else
                {
                    course.EnrollStatus = null;
                }

            }
            return course;
        }


        //for enrolling courses
        public bool EnrollCourse(int userId,Guid courseId)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                SqlCommand command = new SqlCommand("usp_EnrollCourse", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@CourseId", courseId);

                connection.Open();
                result = command.ExecuteNonQuery();
                connection.Close();

            }
            return result > 0 ? true : false;
        }

        //get enroll details
        public List<EnrolDetails> GetEnrolRequest()
        {
            List<EnrolDetails> enrollList = new List<EnrolDetails>();
            using (var connection = CreateConnection())
            {
                SqlCommand command = new SqlCommand("usp_GetEnrollRequest", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    EnrolDetails enroll = new EnrolDetails();
                    enroll.UserId = Convert.ToInt32(dr["userId"]);
                    enroll.CourseId = dr.GetGuid("courseId");
                    enroll.UserName = dr.GetString("userName");
                    enroll.Title = dr.GetString("title");

                    enrollList.Add(enroll);
                }
                connection.Close();
            }
            return enrollList;
        }

        //Aprove or reject enroll request
        public bool UpdateEnroll(int userId, Guid courseId, string status)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                SqlCommand command = new SqlCommand("usp_UpdateEnroll", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@CourseId", courseId);
                command.Parameters.AddWithValue("@EnrollStatus", status);

                connection.Open();
                result = command.ExecuteNonQuery();
                connection.Close();

            }
            return result > 0 ? true : false;
        }

        //Delete Course
        public bool DeleteCourse(Guid courseId)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                SqlCommand command = new SqlCommand("usp_DeleteCourse", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CourseId", courseId);

                connection.Open();
                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return result > 0 ? true : false;
        }

        //update course
        public bool EditCourse(Courses course)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                SqlCommand command = new SqlCommand("usp_UpdateCourse", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Title", course.Title);
                command.Parameters.AddWithValue("@CourseImage", course.CourseImage);
                command.Parameters.AddWithValue("@Description", course.Description);
                command.Parameters.AddWithValue("@CourseFile", course.CourseFile);
                command.Parameters.AddWithValue("@CategoryId", course.CategoryId);
                command.Parameters.AddWithValue("@CourseId", course.CourseId);
                connection.Open();
                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return result > 0 ? true : false;
        }

        //Get enrolled courses
        public List<Courses> GetEnrolledCourses(int userId)
        {
            List<Courses> courseList = new List<Courses>();
            SqlDataReader dr = null;
            try
            {
                using (var connection = CreateConnection())
                {

                    SqlCommand cmd = new SqlCommand("usp_GetEnrolledCourses", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Courses course = new Courses();
                        course.CourseId = dr.GetGuid("courseId");
                        course.Title = dr["title"].ToString();
                        course.CourseImage = dr["courseImage"] as byte[];
                        course.Description = dr["description"].ToString();
                        course.CourseFile = dr["courseFile"].ToString();
                        courseList.Add(course);
                    }

                }
            }

            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }
            }
            return courseList;

        }
           
        }

    }

