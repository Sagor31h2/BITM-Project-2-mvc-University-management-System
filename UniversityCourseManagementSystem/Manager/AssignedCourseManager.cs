using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Gateway;
using UniversityCourseManagementSystem.Models;
using UniversityCourseManagementSystem.Models.ViewModels;

namespace UniversityCourseManagementSystem.Manager
{
    public class AssignedCourseManager
    {
        AssignedCourseGateway aAssignedCourseGateway = new AssignedCourseGateway();
        public string Save(AssignedCourse assignedCourse)
        {
            if (aAssignedCourseGateway.IsCourseExists(assignedCourse))
            {
                return "This course is already assigned";
            }
            else
            {
                int rowAffected = aAssignedCourseGateway.Save(assignedCourse);
                if (rowAffected > 0)
                {
                    return "Course saved";
                }
                return "Save failed";
            }

        }

        public decimal Update(AssignedCourse assignedCourse)
        {
            if (aAssignedCourseGateway.IsCourseExists(assignedCourse))
            {
                return assignedCourse.RemainingCredit;
            }
            else
            {
                var rowAffected = aAssignedCourseGateway.Update(assignedCourse);
                if (rowAffected > 0)
                {
                    return assignedCourse.RemainingCredit - assignedCourse.Credit;
                }
                else
                {
                    return assignedCourse.RemainingCredit;
                }
            }

        }

        public List<ViewCourseModel> GetAllCoursesWithSemesterAndStatus(int departmentId)
        {
            return aAssignedCourseGateway.GetAllCoursesWithSemesterAndStatus(departmentId);
        }
    }
}