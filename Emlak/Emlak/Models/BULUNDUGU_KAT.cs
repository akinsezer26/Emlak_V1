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
    
    public partial class BULUNDUGU_KAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BULUNDUGU_KAT()
        {
            this.KONUT_ISYERI = new HashSet<KONUT_ISYERI>();
        }
    
        public int BULUNDUGU_KAT_ID { get; set; }
        public string BULUNDUGU_KAT1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KONUT_ISYERI> KONUT_ISYERI { get; set; }
    }
}
