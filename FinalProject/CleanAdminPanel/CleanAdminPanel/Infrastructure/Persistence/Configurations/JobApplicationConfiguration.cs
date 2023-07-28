using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class JobApplicationConfiguration : BaseEntityConfiguration<JobApplication>
    {
        public override void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            builder.Property(t => t.Name)
           .HasMaxLength(256)
           .IsRequired();


            builder.Property(t => t.Email)
            .HasMaxLength(256)
            .IsRequired();


            builder.Property(t => t.Phone)
            .HasMaxLength(256)
            .IsRequired();


            builder.Property(t => t.Linkedin)
            .HasMaxLength(256)
            .IsRequired();

            builder.Property(t => t.Cv)
            .IsRequired(false);

            base.Configure(builder);
        }
    }
}
