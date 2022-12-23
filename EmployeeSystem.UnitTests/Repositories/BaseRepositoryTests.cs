using System.Linq.Expressions;
using AutoFixture;
using EmployeeSystem.Domain.Entities;
using EmployeeSystem.Infrastructure.Persistence;
using EmployeeSystem.Infrastructure.Persistence.DataAccess;
using EmployeeSystem.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EmployeeSystem.UnitTests.Repositories;

public class BaseRepositoryTests
{
    private readonly SystemContext _context;
    private readonly Fixture _fixture;
    private readonly BaseRepository<Employee> _sut;

    public BaseRepositoryTests()
    {
        _context = new SystemContext(UnitTestsHelpers.GetInMemoryContextOptions());
        _sut = new EmployeeRepository(_context);

        _fixture = new Fixture();
        _fixture.Customize<Employee>(c => c.Without(e => e.AssignedTasks));

        SeedContext();
    }

    private void SeedContext()
    {
        var employees = _fixture.CreateMany<Employee>(5);

        _context.Employees.AddRange(employees);
        _context.SaveChanges();
    }

    [Fact]
    public async Task Get_ShouldReturnValidDataWithoutFilter()
    {
        // Arrange
        var expected = await _context.Employees.ToListAsync();

        // Act
        var actual = await _sut.GetAsync();

        // Assert
        actual.Should()
              .BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Get_ShouldReturnValidDataWithFilter()
    {
        // Arrange
        Expression<Func<Employee, bool>> filter = e => e.Salary > 0;
        var expected = await _context.Employees.Where(filter).ToListAsync();

        // Act
        var actual = await _sut.GetAsync(filter);

        // Assert
        actual.Should()
              .BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetById_ShouldReturnValidEntity()
    {
        // Arrange
        var id = _context.Employees.First().Id;

        // Act
        var entity = await _sut.GetByIdAsync(id);

        // Assert
        entity.Id
              .Should()
              .Be(id);
    }

    [Fact]
    public async Task Add_ShouldAddNewEntity()
    {
        // Arrange
        var employeeToAdd = _fixture.Create<Employee>();

        // Act
        _sut.Add(employeeToAdd);

        await _context.SaveChangesAsync();

        // Assert
        _context.Employees
                .Should()
                .HaveCount(6);
    }

    [Fact]
    public async Task DeleteById_ShouldDeleteEntity()
    {
        // Arrange
        var employees = _fixture.CreateMany<Employee>(5);
        var id = _context.Employees.First().Id;

        // Act
        await _sut.DeleteByIdAsync(id);
        await _context.SaveChangesAsync();

        // Assert
        _context.Employees
                .Should()
                .NotContain(e => e.Id == id);
    }
}