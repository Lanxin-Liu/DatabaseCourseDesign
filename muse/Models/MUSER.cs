//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace muse.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MUSER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MUSER()
        {
            this.BUYHISTORY = new HashSet<BUYHISTORY>();
            this.COLLETION = new HashSet<COLLETION>();
            this.COMMENTS = new HashSet<COMMENTS>();
            this.FAULT = new HashSet<FAULT>();
            this.FRIENDS = new HashSet<FRIENDS>();
            this.FRIENDS1 = new HashSet<FRIENDS>();
            this.HISTORY = new HashSet<HISTORY>();
            this.HONOR = new HashSet<HONOR>();
            this.SONGLIST = new HashSet<SONGLIST>();
            this.MUSICIANWORKS = new HashSet<MUSICIANWORKS>();
            this.USERSONGLIST = new HashSet<USERSONGLIST>();
        }
    
        public decimal USERID { get; set; }
        public string USERNAME { get; set; }
        public string USERPASSWORD { get; set; }
        public string USERIMAGE { get; set; }
        public string USERSEX { get; set; }
        public string USERBIRTHDAY { get; set; }
        public string USEREMAIL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BUYHISTORY> BUYHISTORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COLLETION> COLLETION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENTS> COMMENTS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAULT> FAULT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FRIENDS> FRIENDS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FRIENDS> FRIENDS1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HISTORY> HISTORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HONOR> HONOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SONGLIST> SONGLIST { get; set; }
        public virtual VIP VIP { get; set; }
        public virtual MUSICIAN MUSICIAN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MUSICIANWORKS> MUSICIANWORKS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USERSONGLIST> USERSONGLIST { get; set; }
    }
}