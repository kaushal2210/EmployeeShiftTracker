//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompanyCard.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PreviousEmployee
    {
        public int PreviousEmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePhoneNo { get; set; }
        public string EmployeeAddress { get; set; }
        public string Email { get; set; }
        public int CompanyCompanyId { get; set; }
    
        public virtual Company Company { get; set; }
    }
}