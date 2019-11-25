using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnrollBCFA.Models
{
    [MetadataType(typeof(AcademicYearMetaData))]
    public partial class tblSchoolYear
    {
        
    }
    public class AcademicYearMetaData
    {
        [Display(Name = "Academic Year")]
        [Required(ErrorMessage = "Please enter an academic year.")]
        public string SchoolYear { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Status field is required")]
        public string status { get; set; }
    }
}