//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GB_Customers.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Customer
    {
        public int customerId { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage ="Insert a Valid Email.")]
        public string email { get; set; }
        public bool active { get; set; }
        public Nullable<System.DateTime> lastUpdate { get; set; }
        public System.DateTime created { get; set; }
        public string distributionGroup { get; set; }
    }
}
