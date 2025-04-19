using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;
using Microsoft.AspNetCore.Http;

namespace Core.Entities.Concrete
{
    public class Product : BaseEntity
    {
        [MaxLength(250)]
        [MinLength(3)]
        [Required]
        public string Name { get; set; }

        [MaxLength(250)]
        [MinLength(3)]
        [Required]
        public string Description { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal UnitPrice { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
