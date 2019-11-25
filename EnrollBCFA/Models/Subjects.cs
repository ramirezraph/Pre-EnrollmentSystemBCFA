using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnrollBCFA.Models
{
    [MetadataType(typeof(SubjectMetaData))]
    public partial class SubjectList
    {
        public string CreateMessage { get; internal set; }
    }
    public class SubjectMetaData
    {
        [Required(ErrorMessage = "Please enter subject code.")]
        [Display(Name = "Subject Code")]
        public string SubjectCode { get; set; }

        [Required(ErrorMessage = "Please enter subject name.")]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }


        [Required(ErrorMessage = "Please enter subject type.")]
        [Display(Name = "Subject Type")]
        public string SubjectType { get; set; }

        [Required(ErrorMessage = "Please enter subject teacher.")]
        [Display(Name ="Subject Teacher")]
        public string SubjectTeacher { get; set; }

        public string CreateMessage { get; set; }
    }
}