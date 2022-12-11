using EmployeeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskStatus = EmployeeSystem.Domain.Entities.TaskStatus;

namespace EmployeeSystem.Infrastructure.Persistence.Configurations;

public class EmployeeTaskConfiguration : IEntityTypeConfiguration<EmployeeTask>
{
    public void Configure(EntityTypeBuilder<EmployeeTask> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Description)
               .HasMaxLength(200);

        builder.Property(t => t.Created)
               .IsRequired();

        builder.Property(t => t.CompleteBy)
               .IsRequired();

        builder.Property(t => t.Status)
               .HasMaxLength(20)
               .HasConversion(
                   s => s.ToString(),
                   s => (TaskStatus) Enum.Parse(typeof(TaskStatus), s));

        builder.HasOne(t => t.Employee)
               .WithMany(e => e.AssignedTasks)
               .HasForeignKey(t => t.EmployeeId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.Manager)
               .WithMany(m => m.CreatedTasks)
               .HasForeignKey(t => t.ManagerId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}