﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineTests.Domain.Entities
{
    using System;
    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
    
    public partial class User
    {
        public User()
        {
            this.UserResults = new HashSet<UserResult>();
        }
    
        public int ID { get; set; }

        [Required(ErrorMessage="აუცილებელი ველი")]
        [MaxLength(100, ErrorMessage = "მომხმარებლის სახელი 50 სიმბოლოზე ნაკლები")]
        [Display(Name="სახელი")]
        public string Name { get; set; }

        [Required(ErrorMessage="აუცილებელი ველი")]
        [Display(Name="ელ-ფოსტა")]
        [MaxLength(100, ErrorMessage = "ელ-ფოსტა 100 სიმბოლოზე ნაკლები")]
        public string Email { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "პაროლი მინიმუმ 6 სიმბოლოსგან შემდგარი და 50 სიმბოლოზე ნაკლები")]
        [Display(Name="პაროლი")]
        public string Password { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [Display(Name="სტატუსი")]
        public string IsActive { get; set; }
    
        public virtual ICollection<UserResult> UserResults { get; set; }
    }
}
