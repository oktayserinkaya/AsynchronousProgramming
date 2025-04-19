using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{
    public class Category : BaseEntity
    {
        [MaxLength(100)]
        [MinLength(3)]
        [Required]
        public string Name { get; set; }
    }
}
