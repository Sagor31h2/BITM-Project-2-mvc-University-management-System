using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Gateway;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Manager
{
    public class StudentManager
    {
        StudentGateway aStudentGateway = new StudentGateway();
        public string Save(Student aStudent)
        {
            if (aStudentGateway.IsEmailExists(aStudent))
            {
                return "Email already exists";
            }
            if (aStudentGateway.IsContactNoExists(aStudent))
            {
                return "Contact no already exists";
            }
            else
            {
                int rowAffected = aStudentGateway.Save(aStudent);
                if (rowAffected > 0)
                {
                    return "Regitration complete";
                }
                return "Registration failed";
            }

        }

        public List<Student> GetDepartmentId(int departmentId)
        {
            return aStudentGateway.GetDepartmentId(departmentId);
        }

        public Department GetDepartmentById(int departmentId)
        {
            return aStudentGateway.GetDepartmentById(departmentId);
        }
    }
}