using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RP7XMC_HFT_2022232.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }
        [StringLength(240)]
        public string CarName { get; set; }
        public int BrandId { get; set; }
        [NotMapped]
        [JsonIgnore]
        //public virtual ICollection<Brand> Brands { get; set; }
        public virtual Brand Brand { get; set; }
        //public Car()
        //{
        //    Brands = new HashSet<Brand>();
        //}
    }
}
