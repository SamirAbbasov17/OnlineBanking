using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class BlogConfiguration : BaseAuditableEntityConfiguration<Blog>
    {
        public override void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(t => t.Content)
                .IsRequired();

            builder.Property(t => t.Description)
                .IsRequired();

            builder.Property(t => t.Image)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
