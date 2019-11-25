using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnrollBCFA.Models
{
    [MetadataType(typeof(AccountMetaData))]
    public partial class Account
    {
        public string LoginErrorMessage { get; internal set; }
    }
    public class AccountMetaData
    {
        public int AccountID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please type your username.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please type your password.")]
        public string Password { get; set; }
        public string LastLogin { get; set; }
        public string AccountCreated { get; set; }


        public string LoginErrorMessage { get; set; }
    }
}