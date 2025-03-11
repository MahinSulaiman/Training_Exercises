using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using WebApplicationSample.Models;
using System.Data;

namespace WebApplicationSample.DAL
{
	public class ProjectDetails_DAL
	{
		string conString = ConfigurationManager.ConnectionStrings["adoConnectionString"].ToString();

		//Get all project details
		public List<ProjectDetails> GetAllProjects()
		{
			List<ProjectDetails> projectList = new List<ProjectDetails>();

			using (SqlConnection connection = new SqlConnection(conString))
			{
				SqlCommand cmd = connection.CreateCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT * FROM Projects";
				SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				DataTable dtProjects = new DataTable();

				connection.Open();
				adapter.Fill(dtProjects);
				connection.Close();

                foreach (DataRow dr in dtProjects.Rows)
                {
					projectList.Add(new ProjectDetails
					{
						ProjectId = Convert.ToInt32(dr["ProjectId"]),
						ProjectName = dr["ProjectName"].ToString(),
						EmployeeId = Convert.ToInt32(dr["EmployeeId"])
					});
                }
            }


			return projectList;
		}

		//Insert projects
		public bool InsertProject(ProjectDetails project) 
		{
			int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
			{
				SqlCommand cmd = new SqlCommand("usp_InsertProject", connection);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
				cmd.Parameters.AddWithValue("@EmployeeId", project.EmployeeId);

				connection.Open();
				id = cmd.ExecuteNonQuery();
				connection.Close();
			}

			if (id > 0){
				return true;
			}
			else
			{
				return false;
			}



        }

        //Get  project details by id
        public ProjectDetails GetProjectById(int id)
		{
			ProjectDetails project = new ProjectDetails();

			using (SqlConnection connection = new SqlConnection(conString))
			{
				SqlCommand cmd = connection.CreateCommand();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "usp_SelectProject";
				cmd.Parameters.AddWithValue("@ProjectId", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dtProjects = new DataTable();

                connection.Open();
                adapter.Fill(dtProjects);
                connection.Close();

                foreach (DataRow dr in dtProjects.Rows)
                {
					project.ProjectId = Convert.ToInt32(dr["ProjectId"]);
					project.ProjectName = dr["ProjectName"].ToString();
					project.EmployeeId = Convert.ToInt32(dr["EmployeeId"]);
                }
				return project;


            }
		}

		//Update project details
        public bool UpdateProject(ProjectDetails project)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("usp_UpdateProject", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectId", project.ProjectId);
                cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                cmd.Parameters.AddWithValue("@EmployeeId", project.EmployeeId);

                connection.Open();
                i = cmd.ExecuteNonQuery();
                connection.Close();
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //delete project details
		public bool DeleteProject(int id)
		{
			int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("usp_DeleteProject", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectId", id);

                connection.Open();
                i = cmd.ExecuteNonQuery();
                connection.Close();
            }
			
			if(i > 0)
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