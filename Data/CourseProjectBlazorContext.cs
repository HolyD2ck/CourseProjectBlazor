using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CourseProjectBlazor.Models;

    public class CourseProjectBlazorContext : DbContext
    {
        public CourseProjectBlazorContext (DbContextOptions<CourseProjectBlazorContext> options)
            : base(options)
        {
        }

        public DbSet<CourseProjectBlazor.Models.Employer> Employer { get; set; } = default!;

    public DbSet<CourseProjectBlazor.Models.Applicant> Applicant { get; set; } = default!;
}
