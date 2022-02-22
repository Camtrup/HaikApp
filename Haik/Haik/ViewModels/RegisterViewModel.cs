using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Haik.ViewModels
{
    public class RegisterViewModel
    {
        
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Compare(nameof(Password), ErrorMessage = "The password fields must match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Date)]
        public string DateOfBirth { get; set; }
        [DataType(DataType.Date)]
        public string Description { get; set; }
        
        [DataType(DataType.Text)]
        public string Gender { get; set;}
        public string APIkey { get; set; }
    }

    
}
