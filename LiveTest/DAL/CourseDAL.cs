using System.Data;
using LiveTest.Models;
using Microsoft.Data.SqlClient;

namespace LiveTest.DAL
{
    public class CourseDAL
    {
        private readonly IConfiguration _configuration;

        // Inject IConfiguration through the constructor
        public CourseDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// List course
        /// </summary>
        /// <returns></returns>
        public List<Courses> GetAllCourses()
        {
            List<Courses> courseList = new List<Courses>();
            using(var connection = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_GetCourses", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Courses course = new Courses();
                        course.CourseId = Convert.ToInt32(dr["courseId"]);
                        course.CourseName = dr["curseName"].ToString();
                        course.CourseDuration = Convert.ToInt32(dr["courseDuration"]);

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
        /// create course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public bool CreateCourse(Courses course)
        {
            int result = 0;
            using(var connection = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_InsertCourse", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                    cmd.Parameters.AddWithValue("@CourseDuration", course.CourseDuration);

                    connection.Open();
                    result = cmd.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }      
              
            }
            return result > 0;
        }

        /// <summary>
        /// get course details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Courses GetCourseById(int id)
        {
            Courses course = new Courses();
            using(var connection = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_GetCourseById", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseId", id);

                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();

                    course.CourseId = Convert.ToInt32(dr["courseId"]);
                    course.CourseName = dr["curseName"].ToString();
                    course.CourseDuration = Convert.ToInt32(dr["courseDuration"]);
                }
                finally
                {
                    connection.Close();
                }
            }

            return course;
        }

        //update course 
        public bool UpdateCourse(Courses course)
        {
            int result = 0;
            using(var connection = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_UpdateCourse", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseId", course.CourseId);
                    cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                    cmd.Parameters.AddWithValue("@CourseDuration", course.CourseDuration);

                    connection.Open();
                    result = cmd.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
            return result > 0;
        }

        //delete course
        public bool DeleteCourse(int id)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_DeleteCourse", (SqlConnection)connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseId", id);
                    connection.Open();
                    result = cmd.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
            return result > 0;
        }
    }
}
