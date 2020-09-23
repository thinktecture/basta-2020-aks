using System;
using System.ComponentModel.DataAnnotations;

namespace Thinktecture.AKS.Sample.Models
{
    public class NewProductItem
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Range(0, Double.MaxValue)]
        public double Price { get; set; }
        [Required]
        [MaxLength(2500)]
        public string Category { get; set; }
    }
}
