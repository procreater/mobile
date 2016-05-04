namespace CordovaSPA.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PageSection")]
    public partial class PageSection
    {
        [Key]
        [StringLength(200)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }

        [StringLength(260)]
        public string ImageName { get; set; }
    }
}
