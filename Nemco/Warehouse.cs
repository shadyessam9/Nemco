namespace Nemco
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Warehouse")]
    public partial class Warehouse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemId { get; set; }

        public int? Quan { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double? ItemProfit { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double? ItemWorth { get; set; }

        public virtual Item Item { get; set; }
    }
}
