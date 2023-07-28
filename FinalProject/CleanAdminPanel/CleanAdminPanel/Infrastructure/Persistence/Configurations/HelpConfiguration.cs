using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class HelpConfiguration : BaseAuditableEntityConfiguration<Help>
    {
        public override void Configure(EntityTypeBuilder<Help> builder)
        {
            builder.Property(t => t.Description)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(t => t.Content)
                .IsRequired();

            builder.Property(t => t.Description)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
