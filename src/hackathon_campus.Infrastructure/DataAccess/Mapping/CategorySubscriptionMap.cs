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
    class CategorySubscriptionMap : IEntityTypeConfiguration<CategorySubscription>
    {
        public void Configure(EntityTypeBuilder<CategorySubscription> builder)
        {
            builder.HasKey(x => new { x.ApplicationUserId, x.CategoryId });
        }
    }
}
