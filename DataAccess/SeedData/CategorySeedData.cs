using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.SeedData
{
    public class CategorySeedData : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
            (
                new Category { Id = 1, Name = "Meyve & Sebze" },
                new Category { Id = 2, Name = "Et & Tavuk" },
                new Category { Id = 3, Name = "Süt & Kahvaltılık" },
                new Category { Id = 4, Name = "Atıştırmalık" }
            );
        }
    }
}
