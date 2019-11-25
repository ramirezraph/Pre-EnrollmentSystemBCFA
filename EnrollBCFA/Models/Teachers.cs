using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnrollBCFA.Models
{
    [MetadataType(typeof(TeacherMetaData))]
    public partial class Teacher
    {
        public string CreateMessage { get; internal set; }
    }
    public class TeacherMetaData
    {
        [Display( Name ="Teacher ID")]
        [Required(ErrorMessage = "Please enter the TeacherID.")]
        public int TeacherID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter the name.")]
        public string TeacherName { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please enter the address.")]
        public string TeacherAddress { get; set; }

        [Display(Name = "Contact")]
        [Required(ErrorMessage = "Please enter the contact number.")]
        public string TeacherContact { get; set; }

        public string CreateMessage { get; set; }
    }
}
