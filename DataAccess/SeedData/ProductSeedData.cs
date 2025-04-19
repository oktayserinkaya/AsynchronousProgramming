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
    public class ProductSeedData : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
            (
                // Meyve & Sebze
                new Product { Id = 1, Name = "Domates", Description = "Taze tarla domatesi", UnitPrice = 15.50m, ImagePath = "domates.jpg", CategoryId = 1 },
                new Product { Id = 2, Name = "Salatalık", Description = "Seradan taze salatalık", UnitPrice = 12.25m, ImagePath = "salatalik.jpg", CategoryId = 1 },
                new Product { Id = 3, Name = "Elma", Description = "Amasya elması", UnitPrice = 10.75m, ImagePath = "elma.jpg", CategoryId = 1 },
                new Product { Id = 4, Name = "Patates", Description = "Yerli patates", UnitPrice = 8.90m, ImagePath = "patates.jpg", CategoryId = 1 },
                new Product { Id = 5, Name = "Soğan", Description = "Kuru soğan", UnitPrice = 7.30m, ImagePath = "sogan.jpg", CategoryId = 1 },

                // Et & Tavuk
                new Product { Id = 6, Name = "Dana Kıyma", Description = "Yağsız dana kıyma", UnitPrice = 210.00m, ImagePath = "kiyma.jpg", CategoryId = 2 },
                new Product { Id = 7, Name = "Tavuk Göğsü", Description = "Taze tavuk fileto", UnitPrice = 85.00m, ImagePath = "tavuk.jpg", CategoryId = 2 },
                new Product { Id = 8, Name = "Sucuk", Description = "Kayseri usulü fermente sucuk", UnitPrice = 120.00m, ImagePath = "sucuk.jpg", CategoryId = 2 },
                new Product { Id = 9, Name = "Kuzu Pirzola", Description = "Kemikli kuzu pirzola", UnitPrice = 280.00m, ImagePath = "kuzupirzola.jpg", CategoryId = 2 },
                new Product { Id = 10, Name = "Hindi Füme", Description = "Dilimli hindi füme", UnitPrice = 95.00m, ImagePath = "hindifume.jpg", CategoryId = 2 },

                // Süt & Kahvaltılık
                new Product { Id = 11, Name = "Süt", Description = "Tam yağlı günlük süt", UnitPrice = 18.50m, ImagePath = "sut.jpg", CategoryId = 3 },
                new Product { Id = 12, Name = "Beyaz Peynir", Description = "Ezine tipi beyaz peynir", UnitPrice = 98.00m, ImagePath = "peynir.jpg", CategoryId = 3 },
                new Product { Id = 13, Name = "Tereyağı", Description = "Kahvaltılık doğal tereyağı", UnitPrice = 140.00m, ImagePath = "tereyag.jpg", CategoryId = 3 },
                new Product { Id = 14, Name = "Zeytin", Description = "Siyah sele zeytin", UnitPrice = 75.00m, ImagePath = "zeytin.jpg", CategoryId = 3 },
                new Product { Id = 15, Name = "Bal", Description = "Süzme çiçek balı", UnitPrice = 160.00m, ImagePath = "bal.jpg", CategoryId = 3 },

                // Atıştırmalık
                new Product { Id = 16, Name = "Cips", Description = "Patates cipsi 150g", UnitPrice = 23.00m, ImagePath = "cips.jpg", CategoryId = 4 },
                new Product { Id = 17, Name = "Çikolata", Description = "Bitter çikolata 80g", UnitPrice = 18.00m, ImagePath = "cikolata.jpg", CategoryId = 4 },
                new Product { Id = 18, Name = "Gofret", Description = "Kakaolu gofret", UnitPrice = 9.00m, ImagePath = "gofret.jpg", CategoryId = 4 },
                new Product { Id = 19, Name = "Gazoz", Description = "Limon aromalı gazoz", UnitPrice = 7.50m, ImagePath = "gazoz.jpg", CategoryId = 4 },
                new Product { Id = 20, Name = "Bisküvi", Description = "Kremalı bisküvi", UnitPrice = 11.75m, ImagePath = "biskuvi.jpg", CategoryId = 4 }
            );
        }
    }
}
