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
    
    public partial class SONGLIST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SONGLIST()
        {
            this.COLLETION = new HashSet<COLLETION>();
            this.SONGLISTMUSIC = new HashSet<SONGLISTMUSIC>();
            this.USERSONGLIST = new HashSet<USERSONGLIST>();
        }
    
        public decimal SONGLISTID { get; set; }
        public string SONGLISTNAME { get; set; }
        public Nullable<decimal> USERID { get; set; }
        public Nullable<decimal> COLLECTIONTIMES { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COLLETION> COLLETION { get; set; }
        public virtual MUSER MUSER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SONGLISTMUSIC> SONGLISTMUSIC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USERSONGLIST> USERSONGLIST { get; set; }
    }
}