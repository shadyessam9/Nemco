namespace Nemco
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Supplier
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupplierId { get; set; }

        [StringLength(255)]
        public string SupplierName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double? Balance { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
