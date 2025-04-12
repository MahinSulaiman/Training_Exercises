using System.Data;
using ClaysysLearningPortal.Models;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;

namespace ClaysysLearningPortal.DAL
{
    public class CoursesDAL
    {

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

        /// <summary>
        /// Get all courses
        /// </summary>
        /// <returns></returns>
        public List<Courses> GetAllCourses()
        {
            List<Courses> courseList = new List<Courses>();
            using (var connection = CreateConnection())
            {
                try
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
                        course.CourseImage = dr["courseImage"] as byte[];
                        course.Description = dr.GetString("description");
                        course.Category = dr.GetString("categoryName");


                        courseList.Add(course);
                    }
                }
                finally
                {
                    connection.Close();
                }
                
            }
            return courseList;
        }

        /// <summary>
        /// fetch list of courses by category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<Courses> GetCoursesByCategory(Guid? categoryId)
        {
                List<Courses> courseList = new List<Courses>();
                using (var connection = CreateConnection())
                {
                try
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
                }
                finally
                {
                    connection.Close();
                }
                    
                }
            return courseList;
        }

        /// <summary>
        /// to add courses
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public bool AddCourse(Courses course)
        {
            int result = 0;
            
            using (var memorystream=new MemoryStream())
            {

                using (var connection = CreateConnection())
                {
                    try
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
                    }
                    finally
                    {
                        connection.Close();
                    }         
                }
            }
            return result > 0 ? true : false;
        }

        /// <summary>
        /// get list of categories
        /// </summary>
        /// <returns></returns>
        public List<Categories> GetCategories()
        {
            List<Categories> categories = new List<Categories>();
            using (var connection = CreateConnection())
            {
                try
                {
                    var command = new SqlCommand("usp_GetCategories", (SqlConnection)connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Categories category = new Categories();
                        category.CategoryId = dr.GetGuid(0);
                        category.Category = dr.GetString(1);

                        categories.Add(category);
                    }
                }
                finally
                {
                    connection.Close();
                }
                
            }
            return categories;
        }

        /// <summary>
        /// get course details by courseId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public Courses GetCourseDetails(int userId,Guid courseId)
        {
            Courses course = new Courses();
            using (var connection = CreateConnection())
            {
                try
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

                    //while (dr.Read())
                    //{
                    dr.Read();
                        course.CourseId = dr.GetGuid("courseId");
                        course.Title = dr["title"].ToString();
                        course.CourseImage = dr["courseImage"] as byte[];
                        course.Description = dr["description"].ToString();
                        course.CourseFile = dr["courseFile"].ToString();
                    //}
                    dr.Close();

                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        course.EnrollStatus = (string)result;
                    }
                    else
                    {
                        course.EnrollStatus = null;
                    }
                }
                finally
                {
                    connection.Close();
                }

            }
            return course;
        }


        /// <summary>
        /// for enrolling courses
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public bool EnrollCourse(int userId,Guid courseId)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                try
                {

                    SqlCommand command = new SqlCommand("usp_EnrollCourse", (SqlConnection)connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@CourseId", courseId);

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
        /// get enroll details
        /// </summary>
        /// <returns></returns>
        public List<EnrolDetails> GetEnrolRequest()
        {
            List<EnrolDetails> enrollList = new List<EnrolDetails>();
            using (var connection = CreateConnection())
            {
                try
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
                }
                finally
                {
                    connection.Close();
                }           
            }
            return enrollList;
        }

        /// <summary>
        /// Aprove or reject enroll request
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateEnroll(int userId, Guid courseId, string status)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("usp_UpdateEnroll", (SqlConnection)connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@CourseId", courseId);
                    command.Parameters.AddWithValue("@EnrollStatus", status);

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
        /// Delete Course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public bool DeleteCourse(Guid courseId)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand("usp_DeleteCourse", (SqlConnection)connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CourseId", courseId);

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
        /// update course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public bool EditCourse(Courses course)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                try
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
                }
                finally
                {
                    connection.Close();
                }
               
            }
            return result > 0;
        }

        //Get enrolled courses
        public List<Courses> GetEnrolledCourses(int userId)
        {
            List<Courses> courseList = new List<Courses>();
                using (var connection = CreateConnection())
                {

                try
                {
                    SqlCommand cmd = new SqlCommand("usp_GetEnrolledCourses", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

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
                finally
                {
                    connection.Close();
                }
                }

            return courseList;
        }
           
        }

    }

