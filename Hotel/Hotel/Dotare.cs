//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dotare
    {
        public int Id_Dotare { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public Nullable<int> Id_Camera { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Camera Camera { get; set; }
    }
}
