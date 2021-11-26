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
    public class ImageMap:IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Path)
                .IsRequired();

            builder.HasOne(x => x.ApplicationUser)
                .WithOne(x => x.Avatar);
            builder.HasOne(x => x.Category)
                .WithOne(x => x.Image);
            builder.HasOne(x => x.Event)
                .WithOne(x => x.Image);
        }


    }
}
