using EmployeeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeSystem.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstName)
               .HasMaxLength(20);

        builder.Property(e => e.LastName)
               .HasMaxLength(20);

        builder.Property(e => e.UserName)
               .HasMaxLength(30);

        builder.Property(e => e.Email)
               .HasMaxLength(30);

        builder.Property(e => e.BirthDate)
               .IsRequired();

        builder.Property(e => e.Salary)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(e => e.Post)
               .HasMaxLength(20)
               .HasConversion(
                   p => p.ToString(), 
                   p => (Post) Enum.Parse(typeof(Post), p));
    }
}