using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PerformanceRatingSystem.Domain.Entities;

namespace PerformanceRatingSystem.Infrastructure.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasData(
            new Department
            {
                DepartmentId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                Name = "Отдел разработки",
            },
            new Department
            {
                DepartmentId = new Guid("c9d4c053-49b6-410c-bc11-2d54a9991870"),
                Name = "Отдел кадров",
            }
        );
    }
}