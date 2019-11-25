using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnrollBCFA.Models
{
    [MetadataType(typeof(EnrollMetaData))]
    public partial class tblEnrollment
    {
        public string RowNumber { get; internal set; }
    }
    public class EnrollMetaData
    {
        public int id { get; set; }

        [Display(Name = "Academic Year")]
        public string AcademicYear { get; set; }

        [Display(Name = "Enrollment ID")]
        public string EnrollmentID { get; set; }

        [Display(Name = "Student ID")]
        public string StudentID { get; set; }

        [Display(Name = "Name")]
        public string NameOfStudent { get; set; }

        [Display(Name = "Type")]
        public string StudentType { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Grade Level")]
        public string GradeLevel { get; set; }

        [Display(Name = "Section")]
        public string Section { get; set; }

        [Display(Name = "Date Enrolled")]
        public string DateEnrolled { get; set; }

        [Display(Name = "No.")]
        public int RowNumber { get; set; }
    }
}