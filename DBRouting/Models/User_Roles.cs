//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBRouting.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_Roles
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
    
        public virtual Master_Roles Master_Roles { get; set; }
        public virtual User User { get; set; }
    }
}
