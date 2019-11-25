using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnrollBCFA.Models
{
    [MetadataType(typeof(StudentMetaData))]
    public partial class tblStudentInformation
    {
        public string CreateMessage { get; internal set; }
    }
    public class StudentMetaData
    {
        [Required(ErrorMessage = "Student ID is Required")]
        [StringLength(11, MinimumLength = 6)]
        [Display(Name = "Student ID")]
        public string StudentID { get; set; }

        [Required(ErrorMessage = "Student Type is Required")]
        [Display(Name = "Type")]
        public string StType { get; set; }

        [Required(ErrorMessage = "Student Last Name is Required")]
        [Display(Name = "Last Name")]
        public string StLastName { get; set; }

        [Required(ErrorMessage = "Student First Name is Required")]
        [Display(Name = "First Name")]
        public string StFirstName { get; set; }

        [Required(ErrorMessage = "Student Middle Name is Required")]
        [Display(Name = "Middle Name")]
        public string StMiddleName { get; set; }

        [Required(ErrorMessage = "Student Age is Required")]
        [Display(Name = "Age")]
        public Nullable<int> StAge { get; set; }

        [Required(ErrorMessage = "Student Birthday is Required")]
        [Display(Name = "Date of Birth")]
        public string StDateOfBirth { get; set; }

        [Required(ErrorMessage = "Student Place of Birth is Required")]
        [Display(Name = "Place of Birth")]
        public string StPlaceOfBirth { get; set; }

        [Required(ErrorMessage = "Student Contact is Required")]
        [Display(Name = "Contact")]
        public string StContact { get; set; }

        [Required(ErrorMessage = "Student Gender is Required")]
        [Display(Name = "Gender")]
        public string StGender { get; set; }

        [Required(ErrorMessage = "Student Marital Status is Required")]
        [Display(Name = "Marital Status")]
        public string StMaritalStatus { get; set; }

        [Required(ErrorMessage = "Student Citizenship is Required")]
        [Display(Name = "Citizenship")]
        public string StCitizenship { get; set; }

        [Required(ErrorMessage = "Student Religion is Required")]
        [Display(Name = "Religion")]
        public string StReligion { get; set; }

        [Required(ErrorMessage = "Student Address is Required")]
        [Display(Name = "Address")]
        public string StAddress { get; set; }

        [Display(Name = "Father's Name")]
        public string StFatherName { get; set; }

        [Display(Name = "Occupation")]
        public string StFatherOccupation { get; set; }

        [Display(Name = "Mother's Name")]
        public string StMotherName { get; set; }

        [Display(Name = "Occupation")]
        public string StMotherOccupation { get; set; }

        [Display(Name = "Parent Address")]
        public string StParentAddress { get; set; }

        [Display(Name = "Elementary")]
        public string StElemSchool { get; set; }

        [Display(Name = "Address")]
        public string StElemAddress { get; set; }

        [Display(Name = "High School")]
        public string StHighSchool { get; set; }

        [Display(Name = "Address")]
        public string StHighAddress { get; set; }

        public string CreateMessage { get; set; }
    }
}