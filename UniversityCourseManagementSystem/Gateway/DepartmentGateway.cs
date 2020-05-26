using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Gateway
{
    public class DepartmentGateway : BaseGateway
    {
        public List<Department> GetAllDepartments()
        {
            string query = "SELECT * FROM Department";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<Department> departments = new List<Department>();
            while (Reader.Read())
            {
                Department aDepartment = new Department();
                aDepartment.Id = (int)Reader["Id"];
                aDepartment.DeptCode = Reader["DeptCode"].ToString();
                aDepartment.DeptName = Reader["DeptName"].ToString();

                departments.Add(aDepartment);
            }

            Reader.Close();
            Connection.Close();
            return departments;
        }
    }
}