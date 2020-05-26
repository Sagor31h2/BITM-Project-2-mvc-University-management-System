﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Models;
using UniversityCourseManagementSystem.Models.ViewModels;

namespace UniversityCourseManagementSystem.Gateway
{
    public class AssignedCourseGateway : BaseGateway
    {
        public bool IsCourseExists(AssignedCourse assignedCourse)
        {

            string query = "SELECT * FROM AssignedCourse WHERE CourseId = '" + assignedCourse.CourseId + "' ";

            Command = new SqlCommand(query, Connection);

            Connection.Open();

            Reader = Command.ExecuteReader();

            bool hasRow = false;

            if (Reader.HasRows)
            {
                hasRow = true;
            }

            Reader.Close();
            Connection.Close();

            return hasRow;
        }
        public int Save(AssignedCourse assignedCourse)
        {

            string query = "INSERT INTO AssignedCourse VALUES(@departmentId,@teacherId,@courseId,@status)";

            Command = new SqlCommand(query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("departmentId", SqlDbType.Int);
            Command.Parameters["departmentId"].Value = assignedCourse.DepartmentId;

            Command.Parameters.Add("teacherId", SqlDbType.Int);
            Command.Parameters["teacherId"].Value = assignedCourse.TeacherId;

            Command.Parameters.Add("courseId", SqlDbType.Int);
            Command.Parameters["courseId"].Value = assignedCourse.CourseId;

            Command.Parameters.Add("status", SqlDbType.Bit);
            Command.Parameters["status"].Value = assignedCourse.Status;

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }
        public int Update(AssignedCourse assignedCourse)
        {

            string query =
                "UPDATE Teacher SET RemainingCredit = " + assignedCourse.RemainingCredit + " - " + assignedCourse.Credit + "  WHERE Id = '" + assignedCourse.TeacherId + "'";

            Command = new SqlCommand(query, Connection);

            Connection.Open();

            var rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;

        }
        public List<ViewCourseModel> GetAllCoursesWithSemesterAndStatus(int departmentId)
        {
            string query = "SELECT * FROM GetAllCoursesWithSemesterAndStatus WHERE Id = '" + departmentId + "'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<ViewCourseModel> courses = new List<ViewCourseModel>();
            while (Reader.Read())
            {
                ViewCourseModel aCourse = new ViewCourseModel();
                aCourse.DepartmentId = (int)Reader["Id"];
                aCourse.CourseCode = Reader["CourseCode"].ToString();
                aCourse.CourseName = Reader["CourseName"].ToString();
                aCourse.Semester = Reader["Semester"].ToString();
                aCourse.TeacherName = Reader["Name"].ToString();
                aCourse.Status = Reader["Status"].ToString();
                if (aCourse.Status == "")
                {
                    aCourse.AssignedStatus = "Not Assigned Yet";
                }
                else
                {
                    aCourse.AssignedStatus = aCourse.TeacherName;
                }

                courses.Add(aCourse);
            }

            Reader.Close();
            Connection.Close();
            return courses;
        }
    }
}