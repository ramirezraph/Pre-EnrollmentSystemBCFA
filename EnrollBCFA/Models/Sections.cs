using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnrollBCFA.Models
{
    [MetadataType(typeof(SectionMetaData))]
    public partial class tblSection
    {
        public string CreateMessage { get; internal set; }
    }
    public class SectionMetaData
    {
        [Required(ErrorMessage = "Please enter Type.")]
        [Display(Name = "Student Type")]
        public string SType { get; set; }

        [Required(ErrorMessage = "Please enter Section ID.")]
        [Display(Name = "Section ID")]
        public int SectionID { get; set; }
        
        [Required(ErrorMessage = "Please enter Grade Level.")]
        [Display(Name = "Grade Level")]
        public string GradeLevel { get; set; }

        [Required(ErrorMessage = "Please enter Section Name.")]
        [Display(Name = "Section Name")]
        public string SectionName { get; set; }

        [Required(ErrorMessage = "Please enter Adviser.")]
        [Display(Name = "Adviser")]
        public string SectionAdviser { get; set; }

        public string CreateMessage { get; set; }
    }
}