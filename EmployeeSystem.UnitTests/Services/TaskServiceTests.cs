using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using EmployeeSystem.Application.Interfaces.Persistence;
using EmployeeSystem.Application.Interfaces.Services;
using EmployeeSystem.Application.Models;
using EmployeeSystem.Domain.Entities;
using EmployeeSystem.Infrastructure.Services;
using EmployeeSystem.UnitTests.Helpers;
using FluentAssertions;
using Moq;
using Xunit;

namespace EmployeeSystem.UnitTests.Services;

public class TaskServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;

    private readonly TaskService _sut;

    public TaskServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _mapper = UnitTestsHelpers.CreateMapperProfile();
        _sut = new TaskService(_unitOfWorkMock.Object, _currentUserServiceMock.Object, _mapper);

        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Theory, AutoData]
    public async Task GetEmployeeTasks_ShouldReturnValidData(string userName)
    {
        // Arrange
        var tasks = _fixture.CreateMany<EmployeeTask>(5);

        _unitOfWorkMock.Setup(x => x.TaskRepository.GetEmployeeTasksAsync(It.IsAny<string>()))
                       .ReturnsAsync(tasks);

        var expected = _mapper.Map<IEnumerable<TaskDto>>(tasks);

        // Act
        var actual = await _sut.GetEmployeeTasksAsync(userName);

        // Assert
        actual.Should()
              .BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetCurrentTasks_ShouldReturnValidData()
    {
        // Arrange
        var userName = _fixture.Create<string>();
        var tasks = _fixture.CreateMany<EmployeeTask>(5);
        var expected = _mapper.Map<IEnumerable<TaskDto>>(tasks);

        _currentUserServiceMock.Setup(x => x.GetUserName())
                               .Returns(userName);

        _unitOfWorkMock.Setup(x => x.TaskRepository.GetEmployeeTasksAsync(It.IsAny<string>()))
                       .ReturnsAsync(tasks);

        // Act
        var actual = await _sut.GetCurrentTasksAsync();

        // Assert
        actual.Should()
              .BeEquivalentTo(expected);
    }
}