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
    
    public partial class Category
    {
        public Category()
        {
            this.Tests = new HashSet<Test>();
        }
    
        public int ID { get; set; }
        [Required(ErrorMessage = "აუცილებელი ველი")]
        [Display(Name="კატეგორია")]
        public string Name { get; set; }
    
        public virtual ICollection<Test> Tests { get; set; }
    }
}
