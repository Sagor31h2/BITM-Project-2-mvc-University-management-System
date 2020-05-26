using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code Field Can't Be Empty")]
        [MinLength(5, ErrorMessage = "Code must be at least five character")]
        public string CourseCode { get; set; }
        [Required(ErrorMessage = "Name Field Can't Be Empty")]
        public string CourseName { get; set; }
        [Required(ErrorMessage = "Credit Field Can't Be Empty")]
        [Range(0.5, 5, ErrorMessage = "Credit must be between .5 to 5")]

        public decimal Credit { get; set; }
        [Required(ErrorMessage = "Description Field Can't Be Empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Department Field Can't Be Empty")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Semester Field Can't Be Empty")]
        public int SemesterId { get; set; }
    }
}