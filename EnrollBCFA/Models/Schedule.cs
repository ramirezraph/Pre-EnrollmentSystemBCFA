using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnrollBCFA.Models
{
    [MetadataType(typeof(ScheduleMetaData))]
    public partial class tblSchedule
    {
        
    }
    public partial class ScheduleMetaData
    {
        [Required(ErrorMessage = "A schedule needs an ID.")]
        [Display(Name = "Schedule ID")]
        public int SchedID { get; set; }

        [Required(ErrorMessage = "Academic Year is required to create schedule.")]
        [Display(Name = "Academic Year")]
        [MinLength(9, ErrorMessage = "Academic year format is wrong")]
        [MaxLength(9, ErrorMessage = "Academic year format is wrong")]
        public string AcademicYear { get; set; }

        [Required(ErrorMessage = "Student type is required.")]
        [Display(Name = "Student Type")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Please select the grade level.")]
        [Display(Name = "Grade Level")]
        public string GradeLevel { get; set; }

        [Required(ErrorMessage = "Please select the section.")]
        [Display(Name = "Section")]
        public string Section { get; set; }

        public string Adviser { get; set; }

        public string SubjectCode { get; set; }

        public string DescriptiveTitle { get; set; }

        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Saturday { get; set; }

        public string Day { get; set; }

        public string Time { get; set; }

        public string TimeFrom { get; set; }

        public string TimeTo { get; set; }

        public string TeacherID { get; set; }

        public string Teacher { get; set; }
    }
}