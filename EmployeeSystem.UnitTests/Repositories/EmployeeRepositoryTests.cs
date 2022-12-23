using AutoFixture;
using AutoFixture.Xunit2;
using EmployeeSystem.Domain.Entities;
using EmployeeSystem.Infrastructure.Persistence;
using EmployeeSystem.Infrastructure.Persistence.DataAccess;
using EmployeeSystem.UnitTests.Helpers;
using FluentAssertions;
using Xunit;

namespace EmployeeSystem.UnitTests.Repositories;

public class EmployeeRepositoryTests
{
    private readonly SystemContext _context;
    private readonly EmployeeRepository _sut;
    private readonly Fixture _fixture;

    public EmployeeRepositoryTests()
    {
        _context = new SystemContext(UnitTestsHelpers.GetInMemoryContextOptions());
        _sut = new EmployeeRepository(_context);

        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Theory, AutoData]
    public async Task GetByUserName_ShouldReturnCorrectValue(string username)
    {
        // Arrange
        var expected = _fixture.Build<Employee>()
                                        .With(e => e.UserName, username)
                                        .Create();

        _context.Employees.AddRange(_fixture.CreateMany<Employee>(5));
        _context.Employees.Add(expected);
        await _context.SaveChangesAsync();

        // Act
        var actual = await _sut.GetByUserNameAsync(username);

        // Assert
        actual.Should()
              .BeEquivalentTo(expected);
    }
}