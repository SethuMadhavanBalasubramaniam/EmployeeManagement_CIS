using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EmployeeManagement_CIS.Models;

namespace EmployeeManagement_CIS.DataAccessLayer
{
    public class EmployeeDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        //Get All Products

        public List<EmployeeModel> GetExperienceLevel()
        {
            List<EmployeeModel> employee = new List<EmployeeModel>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_GET_MASTER_EXP_LEVEL";

                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                connection.Open();
                sda.Fill(dt);
                connection.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    employee.Add(new EmployeeModel
                    {
                        EmployeeExperienceLevel_ID = Convert.ToInt32(dr["ELM_EXP_LEVEL_ID"]),
                        EmployeeExperienceLevel = dr["ELM_LEVEL_NAME"].ToString()
                    });

                }
            }

            return employee;
        }

        public List<EmployeeModel> GetGender()
        {
            List<EmployeeModel> employee = new List<EmployeeModel>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_GET_MASTER_GENDER";

                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                connection.Open();
                sda.Fill(dt);
                connection.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    employee.Add(new EmployeeModel
                    {
                        EmployeeGender_ID = Convert.ToInt32(dr["MG_GENDER_ID"]),
                        EmployeeGender = dr["MG_GENDER_NAME"].ToString()
                    });

                }
            }

            return employee;
        }

        public DataTable InsertUpdateEmployeeDetails(int EmployeeID, string EmployeeName, string EmployeeEmailID, string EmployeeDOB,
                    int EmployeeExperienceLevel_ID, int EmployeeGender_ID, string EmployeeAddress)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_INSERT_EMPLOYEE_DETAILS";
                command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                command.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                command.Parameters.AddWithValue("@EmployeeEmailID", EmployeeEmailID);
                command.Parameters.AddWithValue("@EmployeeDOB", EmployeeDOB);
                command.Parameters.AddWithValue("@EmployeeExperienceLevel", EmployeeExperienceLevel_ID);
                command.Parameters.AddWithValue("@EmployeeGender", EmployeeGender_ID);
                command.Parameters.AddWithValue("@EmployeeAddress", EmployeeAddress);

                SqlDataAdapter sda = new SqlDataAdapter(command);
                
                connection.Open();
                sda.Fill(dt);
                connection.Close();
            }

            return dt;
        }

        public DataTable GetEmployeeDetails(int offset, int PageSize, string SearchKeyword)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_GET_EMPLOYEE_DETAILS";
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@PageSize", PageSize);
                command.Parameters.AddWithValue("@SearchKeyword", SearchKeyword);

                SqlDataAdapter sda = new SqlDataAdapter(command);

                connection.Open();
                sda.Fill(dt);
                connection.Close();
            }

            return dt;
        }
        
    }
}