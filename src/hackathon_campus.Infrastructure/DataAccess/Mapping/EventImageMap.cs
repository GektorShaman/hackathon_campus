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
    public class EventImageMap : IEntityTypeConfiguration<EventImage>
    {
        public void Configure(EntityTypeBuilder<EventImage> builder)
        {
            builder.HasKey(x => x.EventId);
            builder.HasKey(x => x.ImageId);

            builder.HasOne(x => x.Event)
                .WithOne(x => x.Image)
                .HasForeignKey<EventImage>(x => x.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Image)
                .WithMany(x => x.EventImages)
                .HasForeignKey(x => x.ImageId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }

}
