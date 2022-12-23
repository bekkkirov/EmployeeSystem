using AutoFixture;
using AutoFixture.Xunit2;
using EmployeeSystem.Domain.Entities;
using EmployeeSystem.Infrastructure.Persistence.DataAccess;
using EmployeeSystem.Infrastructure.Persistence;
using EmployeeSystem.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EmployeeSystem.UnitTests.Repositories;

public class TaskRepositoryTests
{
    private readonly SystemContext _context;
    private readonly TaskRepository _sut;
    private readonly Fixture _fixture;

    public TaskRepositoryTests()
    {
        _context = new SystemContext(UnitTestsHelpers.GetInMemoryContextOptions());
        _sut = new TaskRepository(_context);

        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Theory, AutoData]
    public async Task GetEmployeeTasks_ShouldReturnValidData(string userName)
    {
        // Arrange
        var employee = _fixture.Build<Employee>()
                               .With(e => e.UserName, userName)
                               .Without(e => e.AssignedTasks)
                               .Create();

        var tasks = _fixture.CreateMany<EmployeeTask>(5);

        var expected = _fixture.Build<EmployeeTask>()
                                               .With(t => t.Employee, employee)
                                               .CreateMany(3)
                                               .ToList();

        _context.Tasks.AddRange(tasks);
        _context.Tasks.AddRange(expected);
        await _context.SaveChangesAsync();

        // Act
        var actual = await _sut.GetEmployeeTasksAsync(userName);

        // Assert
        actual.Should()
              .BeEquivalentTo(expected);
    }
}