using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Blog> Blogs => Set<Blog>();

        public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();

        public DbSet<Help> Helps => Set<Help>();

        public DbSet<Job> Jobs => Set<Job>();

        public DbSet<JobApplication> JobApplications => Set<JobApplication>();

        public DbSet<JobRequirement> JobRequirements => Set<JobRequirement>();

        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;


        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(
       DbContextOptions<ApplicationDbContext> options,
       AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
        {
            this._auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                "Server=DESKTOP-9OSB66T;Database=AdminPanel;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
            }
            base.OnConfiguring(optionsBuilder);

            
        }
    }
}
