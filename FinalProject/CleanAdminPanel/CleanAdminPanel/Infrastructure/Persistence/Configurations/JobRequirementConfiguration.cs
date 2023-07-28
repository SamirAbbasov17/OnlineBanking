using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class JobRequirementConfiguration : BaseAuditableEntityConfiguration<JobRequirement>
    {
        public override void Configure(EntityTypeBuilder<JobRequirement> builder)
        {
            builder.Property(t => t.Name)
              .IsRequired();


            builder.HasOne(t => t.Job).WithMany(x => x.JobRequirementList).HasForeignKey(y => y.JobId)
                .IsRequired(false);

            base.Configure(builder);
        }
    }
}
