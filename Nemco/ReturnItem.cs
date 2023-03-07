namespace Nemco
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ReturnItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReturnId { get; set; }

        public int? ItemId { get; set; }

        public virtual Item Item { get; set; }

        public virtual Return Return { get; set; }
    }
}
