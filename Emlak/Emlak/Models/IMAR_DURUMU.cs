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
    
    public partial class IMAR_DURUMU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IMAR_DURUMU()
        {
            this.ARSA_TARLA = new HashSet<ARSA_TARLA>();
        }
    
        public int IMAR_DURUMU_ID { get; set; }
        public string IMAR_DURUMU1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ARSA_TARLA> ARSA_TARLA { get; set; }
    }
}
