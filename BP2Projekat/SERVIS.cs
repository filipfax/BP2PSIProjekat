//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BP2Projekat
{
    using System;
    using System.Collections.Generic;
    
    public partial class SERVIS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SERVIS()
        {
            this.RADNIKs = new HashSet<RADNIK>();
            this.SERVISNI_ALAT = new HashSet<SERVISNI_ALAT>();
        }
    
        public int SERV_ID { get; set; }
        public string WEB_STR { get; set; }
        public int BR_ZAP { get; set; }
        public int TELBROJ { get; set; }
        public string ADRESA { get; set; }
        public string RAD_VRM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RADNIK> RADNIKs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SERVISNI_ALAT> SERVISNI_ALAT { get; set; }
    }
}
