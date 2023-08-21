using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class JobConfiguration : BaseAuditableEntityConfiguration<Job>
    {
        public override void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.Property(t => t.JobName)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(t => t.JobTitle)
                .IsRequired();

            builder.Property(t => t.JobTime)
                .IsRequired();

            builder.Property(t => t.JobDescription)
               .IsRequired();

            builder.Property(t => t.Experience)
              .IsRequired(false);

            base.Configure(builder);
        }
    }
}
