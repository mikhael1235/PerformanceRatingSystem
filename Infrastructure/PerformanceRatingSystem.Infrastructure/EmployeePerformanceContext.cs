using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;
using PerformanceRatingSystem.Infrastructure.Configurations;

namespace PerformanceRatingSystem.Infrastructure;

public partial class EmployeePerformanceContext(DbContextOptions<EmployeePerformanceContext> options) 
    : IdentityDbContext<User>(options)
{

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<ActualPerformanceResult> ActualPerformanceResults { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DepartmentPerformanceIndicator> DepartmentPerformanceIndicators { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeePerformanceIndicator> EmployeePerformanceIndicators { get; set; }

    public virtual DbSet<PlannedPerformanceValue> PlannedPerformanceValues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
    }
}
