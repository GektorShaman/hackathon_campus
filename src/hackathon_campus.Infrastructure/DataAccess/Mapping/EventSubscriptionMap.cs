using System;
using System.Collections.Generic;
using System.Linq;
using hackathon_campus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Infrastructure.DataAccess.Mapping
{
    public class EventSubscriptionMap : IEntityTypeConfiguration<EventSubscription>
    {
        public void Configure(EntityTypeBuilder<EventSubscription> builder)
        {
            builder.HasKey(x => new { x.ApplicationUserId, x.EventId });
        }
    }
}
