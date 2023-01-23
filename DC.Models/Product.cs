using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(30)]

        public string Name { get; set; }
        [Required, StringLength(50)]

        public string Description { get; set; }
        [Required, StringLength(255)]
        public string Colour { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Size { get; set; }
        [Required, StringLength(30)]
        public string Material { get; set; }

        [Range(0, 6)]
        public int Type { get; set; } = 0;
        [Required]
        public decimal DiscountedPrice { get; set; }
        [ValidateNever]

        public string ImageURL { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [Required]
        [Display(Name="ShoesType")]
        public int CoverTypeId { get; set; }
        [ValidateNever]
        public CoverType CoverType { get; set; }
    }
}
