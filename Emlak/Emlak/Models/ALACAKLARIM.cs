//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Emlak.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ALACAKLARIM
    {
        public long AlacakID { get; set; }
        public Nullable<System.DateTime> tarih { get; set; }
        public string Kimden { get; set; }
        public Nullable<int> miktar { get; set; }
        public string BorcluCep { get; set; }
        public Nullable<System.DateTime> OdemeTarihi { get; set; }
        public string Note { get; set; }
        public long UserID { get; set; }
    
        public virtual USERS USERS { get; set; }
    }
}
