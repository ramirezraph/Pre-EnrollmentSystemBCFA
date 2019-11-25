using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnrollBCFA.Models
{
    [MetadataType(typeof(RequirementsMetaData))]
    public partial class tblRequirement
    {
        
    }
    public class RequirementsMetaData
    {
        [Display(Name = "Student ID")]
        public string StudentID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "F137")]
        public bool Form137 { get; set; }

        [Display(Name = "NSO")]
        public bool NSO { get; set; }

        [Display(Name = "GM")]
        public bool GoodMoral { get; set; }

        [Display(Name = "GC")]
        public bool GradeCard { get; set; }
    }
}




