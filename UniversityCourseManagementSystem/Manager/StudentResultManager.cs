using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Gateway;
using UniversityCourseManagementSystem.Models;
using UniversityCourseManagementSystem.Models.ViewModels;

namespace UniversityCourseManagementSystem.Manager
{
    public class StudentResultManager
    {
        StudentResultGateway aStudentResultGateway = new StudentResultGateway();

        public List<Grade> GetAllGrades()
        {
            return aStudentResultGateway.GetAllGrades();
        } 
        public List<ViewStudentModel> GetEnrolledCourses(int studentId)
        {
            return aStudentResultGateway.GetEnrolledCourses(studentId);
        }

        public  string Save(StudentResult studentResult)
        {
            if (aStudentResultGateway.CheckResultOfSameCourseBySameStudent(studentResult))
            {
                return "This course's result is saved for the same student";
            }
            else
            {
                int rowAffected = aStudentResultGateway.Save(studentResult);
                if (rowAffected > 0)
                {
                    return "Saved Successfully";
                }
                return "Saving Failed";
            }
          
        }
    }
}