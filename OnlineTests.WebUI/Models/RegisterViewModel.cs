using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTests.WebUI.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage="გთხოვთ შეიყვანოთ მომხმარებლის სახელი")]
        [Display(Name="მომხმარებლის სახელი")]
        [MaxLength(50, ErrorMessage = "მომხმარებლის სახელი 50 სიმბოლოზე ნაკლები")]
        public string Username { get; set; }

        [Required(ErrorMessage = "გთხოვთ შეიყვანოთ ელ-ფოსტა")]
        [DataType(DataType.EmailAddress, ErrorMessage="გთხოვთ შეიყვანოთ სწორად ელ-ფოსტა")]
        [Display(Name = "ელ-ფოსტა")]
        [MaxLength(50, ErrorMessage = "ელ-ფოსტა 50 სიმბოლოზე ნაკლები")]
        public string Email { get; set; }

        [Required(ErrorMessage = "გთხოვთ შეიყვანოთ პაროლი")]
        [StringLength(50, MinimumLength=6, ErrorMessage="პაროლი მინიმუმ 6 სიმბოლოსგან შემდგარი და 50 სიმბოლოზე ნაკლები")]
        [Display(Name = "პაროლი")]
        public string Password { get; set; }

        [Required(ErrorMessage = "გთხოვთ შეიყვანოთ დამადასტურებელი პაროლი")]
        [Display(Name = "დამადასტურებელი პაროლი")]
        public string ConfirmPassword { get; set; }
    }
}