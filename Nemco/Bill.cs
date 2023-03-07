namespace Nemco
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            Returns = new HashSet<Return>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillId { get; set; }

        [StringLength(255)]
        public string agent { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(30)]
        public string DateTime { get; set; }

        [StringLength(255)]
        public string Customer { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double? Profit { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double? Total { get; set; }

        public virtual BillItem BillItem { get; set; }

        public virtual Discount Discount { get; set; }

        public virtual Profit Profit1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Return> Returns { get; set; }

        public virtual Sale Sale { get; set; }
    }
}
