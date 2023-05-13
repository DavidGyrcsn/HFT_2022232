using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RP7XMC_HFT_2022232.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }
        [StringLength(240)]
        public string CarName { get; set; }
        [NotMapped]
        public virtual ICollection<Brand> Brands { get; set;}

    }
}
