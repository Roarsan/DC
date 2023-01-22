using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(30)]

        public string Name { get; set; } = string.Empty;
        [Required, StringLength(50)]

        public string Description { get; set; } = string.Empty;
        [Required, StringLength(255)]
        public string Colour { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Size { get; set; }
        [Required, StringLength(30)]
        public string Material { get; set; } = string.Empty;

        [Range(0, 6)]
        public int Type { get; set; } = 0;
        [Required]
        public decimal DiscountedPrice { get; set; }

        public string ImageURL { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [Required]
        public int CoverTypeId { get; set; }
        public CoverType CoverType { get; set; }
    }
}
