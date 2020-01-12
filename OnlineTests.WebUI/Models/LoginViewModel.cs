using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTests.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="გთხოვთ შეიყვანოთ მომხმარებლის სახელი")]
        [Display(Name="მომხმარებელის სახელი")]
        public string Username { get; set; }

        [Required(ErrorMessage="გთხოვთ შეიყვანოთ პაროლი")]
        [Display(Name="პაროლი")]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }
    }

    public enum Role { მომხმარებელი, ადმინისტრატორი }
}