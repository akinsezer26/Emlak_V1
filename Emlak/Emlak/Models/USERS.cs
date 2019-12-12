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
    
    public partial class USERS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USERS()
        {
            this.AJANDA = new HashSet<AJANDA>();
            this.ALACAKLARIM = new HashSet<ALACAKLARIM>();
            this.ARSA_TARLA = new HashSet<ARSA_TARLA>();
            this.BINA = new HashSet<BINA>();
            this.BORCLARIM = new HashSet<BORCLARIM>();
            this.EmlakArayanlar = new HashSet<EmlakArayanlar>();
            this.KONUT_ISYERI = new HashSet<KONUT_ISYERI>();
            this.MESAJ = new HashSet<MESAJ>();
            this.MESAJ1 = new HashSet<MESAJ>();
            this.MUSTERI = new HashSet<MUSTERI>();
        }
    
        public long UserID { get; set; }
        public string UserNickName { get; set; }
        public string UserPassword { get; set; }
        public int UserType { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string CepTelefonu { get; set; }
        public string IsTelefonu { get; set; }
        public string Unvan { get; set; }
        public string iban { get; set; }
        public string adres { get; set; }
        public string ppicture { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AJANDA> AJANDA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ALACAKLARIM> ALACAKLARIM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ARSA_TARLA> ARSA_TARLA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BINA> BINA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BORCLARIM> BORCLARIM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmlakArayanlar> EmlakArayanlar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KONUT_ISYERI> KONUT_ISYERI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MESAJ> MESAJ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MESAJ> MESAJ1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MUSTERI> MUSTERI { get; set; }
        public virtual USERTYPES USERTYPES { get; set; }
        public string LoginErrorMessage { get; internal set; }
    }
}