using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP7XMC_HFT_2022232.Models
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }
        [StringLength(240)]
        public string BrandName { get; set; }
        public int CarId { get; set; }
        public int ServiceId { get; set;}
        [NotMapped]
        public Car Car { get; set; }
        [NotMapped]
        public Service Service { get; set; }
    }
}
