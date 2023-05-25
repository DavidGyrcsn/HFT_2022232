using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        public int MaintenanceCost { get; set; }
        [NotMapped]
        //[JsonIgnore]
        public Car Car { get; set; }
        [NotMapped]
        //[JsonIgnore]
        public Service Service { get; set; }
        
    }
}
