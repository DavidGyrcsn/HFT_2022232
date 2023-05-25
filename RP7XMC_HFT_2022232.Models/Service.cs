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
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }
        [StringLength(240)]
        public string ServiceName { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Brand> Brands { get; set; }
    }
}
