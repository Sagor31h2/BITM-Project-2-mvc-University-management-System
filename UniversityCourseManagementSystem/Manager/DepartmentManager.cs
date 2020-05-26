using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Gateway;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Manager
{
    public class DepartmentManager
    {
        DepartmentGateway aDepartmentGateway = new DepartmentGateway();
        public List<Department> GetAllDepartments()
        {
            return aDepartmentGateway.GetAllDepartments();
        }
         public List<SelectListItem> GetDepartmentsForDropDownList()
        {
            var departments = GetAllDepartments();

            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem() {Value = "", Text = "--Select--"}
            };

            foreach (Department item in departments)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.DeptName
                };
                items.Add(selectListItem);
            }
            return items;
        }
    }
}