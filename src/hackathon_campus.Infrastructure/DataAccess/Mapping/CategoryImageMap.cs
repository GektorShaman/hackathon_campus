using hackathon_campus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Infrastructure.DataAccess.Mapping
{
    class CategoryImageMap : IEntityTypeConfiguration<CategoryImage>
    {
        public void Configure(EntityTypeBuilder<CategoryImage> builder)
        {
            builder.HasKey(x => x.CategoryId);
            builder.HasKey(x => x.ImageId);

            builder.HasOne(x => x.Category)
                .WithOne(x => x.Image)
                .HasForeignKey<CategoryImage>(x => x.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Image)
                .WithMany(x => x.CategoryImages)
                .HasForeignKey(x => x.ImageId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
