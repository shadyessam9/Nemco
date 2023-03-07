namespace Nemco
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupplierId { get; set; }

        public double? Debit { get; set; }

        public double? Credit { get; set; }

        [StringLength(30)]
        public string DateTime { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
