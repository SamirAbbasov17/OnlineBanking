using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class ContactMessageConfiguration : BaseEntityConfiguration<ContactMessage>
    {
        public override void Configure(EntityTypeBuilder<ContactMessage> builder)
        {
            builder.Property(t => t.Name)
           .HasMaxLength(256)
           .IsRequired();


            builder.Property(t => t.Email)
            .HasMaxLength(256)
            .IsRequired();


            builder.Property(t => t.Phone)
            .HasMaxLength(256)
            .IsRequired(false);


            builder.Property(t => t.Company)
            .HasMaxLength(256)
            .IsRequired(false);

            builder.Property(t => t.MessageTitle)
            .IsRequired();

            builder.Property(t => t.MessageBody)
            .IsRequired();

            base.Configure(builder);
        }
    }
}
