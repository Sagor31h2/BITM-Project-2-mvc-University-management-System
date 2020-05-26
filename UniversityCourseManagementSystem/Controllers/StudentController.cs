using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.UI;
using UniversityCourseManagementSystem.Manager;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        DepartmentManager aDepartmentManager = new DepartmentManager();
        StudentManager aStudentManager = new StudentManager();
       
        public ActionResult Student()
        {
            ViewBag.Departments = aDepartmentManager.GetDepartmentsForDropDownList();
            return View();
        }

        [HttpPost] 
        
        public ActionResult Student(Student aStudent)
        {
            aStudent.RegistrationNo = GetRegistrationNumber(aStudent.DepartmentId);
            ViewBag.Departments = aDepartmentManager.GetDepartmentsForDropDownList();
            ViewData["Student"] = aStudent;
            ViewBag.Message = aStudentManager.Save(aStudent);
            return PartialView("ViewStudentInfo");
        }
        public ActionResult ViewStudentInfo()
        {
            ViewBag.Student = ViewData["Student"];
            ViewBag.Department = ViewData["Department"];
            return View("Student");
        }
        
        private string GetRegistrationNumber(int departmentId)
        {
            Department aDepartment = aStudentManager.GetDepartmentById(departmentId);
            ViewData["Department"] = aDepartment;
            Student student = new Student();
            List<Student> lastStudent = aStudentManager.GetDepartmentId(departmentId);
            Student aStudent  = lastStudent.LastOrDefault();
            if (aStudent == null)
            {
                student.RegistrationNo = aDepartment.DeptCode + "-" + DateTime.Now.Year + "-" + "001";
            }

            else
            {
                student.RegistrationNo = aDepartment.DeptCode + "-" + DateTime.Now.Year + "-" + (Convert.ToInt32(aStudent.RegistrationNo.Substring(9, aStudent.RegistrationNo.Length - 9)) + 1).ToString("D3");
            }

            return student.RegistrationNo;
        }
	}
}