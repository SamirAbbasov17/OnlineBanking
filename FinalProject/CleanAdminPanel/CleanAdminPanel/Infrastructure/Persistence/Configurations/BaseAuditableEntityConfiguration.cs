using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class BaseAuditableEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseAuditableEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(t => t.Id).IsRequired();
            builder.Property(t => t.Created).IsRequired();
            builder.Property(t => t.CreatedBy).HasMaxLength(150).IsRequired(false);
            builder.Property(t => t.LastModified).IsRequired(false);
            builder.Property(t => t.LastModifiedBy).HasMaxLength(150).IsRequired(false);
        }
    }
}
