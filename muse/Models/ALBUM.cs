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
    
    public partial class ALBUM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ALBUM()
        {
            this.MUSIC = new HashSet<MUSIC>();
        }
    
        public decimal ALBUMID { get; set; }
        public string ALBUMNAME { get; set; }
        public string ALBUMIMAGE { get; set; }
        public string ALBUMPBTIME { get; set; }
        public Nullable<decimal> SINGERID { get; set; }
        public string ALBUMINTRO { get; set; }
    
        public virtual SINGER SINGER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MUSIC> MUSIC { get; set; }
    }
}