using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Blog> Blogs { get; }
        DbSet<ContactMessage> ContactMessages { get; }
        DbSet<Help> Helps { get; }
        DbSet<Job> Jobs { get; }
        DbSet<JobApplication> JobApplications { get; }
        DbSet<JobRequirement> JobRequirements { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

