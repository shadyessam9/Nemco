namespace Nemco
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            BillItems = new HashSet<BillItem>();
            ReturnItems = new HashSet<ReturnItem>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemId { get; set; }

        [StringLength(255)]
        public string ItemName { get; set; }

        public double? Cost { get; set; }

        public double? Profit { get; set; }

        public int? DeptId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillItem> BillItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReturnItem> ReturnItems { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}
