using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using EmployeeSystem.Application.Exceptions;
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

public class EmployeeServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;

    private readonly EmployeeService _sut;

    public EmployeeServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _mapper = UnitTestsHelpers.CreateMapperProfile();
        _sut = new EmployeeService(_unitOfWorkMock.Object, _currentUserServiceMock.Object, _mapper);

        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Theory, AutoData]
    public async Task GetEmployeeProfile_ShouldReturnValidData(string userName)
    {
        // Arrange
        var employee = _fixture.Create<Employee>();

        _unitOfWorkMock.Setup(x => x.EmployeeRepository.GetByUserNameAsync(It.IsAny<string>()))
                       .ReturnsAsync(employee);

        var expected = _mapper.Map<EmployeeDto>(employee);

        // Act
        var actual = await _sut.GetEmployeeProfileAsync(userName);

        // Assert
        actual.Should()
              .BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetEmployeeProfile_ShouldThrowWhenNotFound()
    {
        var userName = _fixture.Create<string>();

        _unitOfWorkMock.Setup(x => x.EmployeeRepository.GetByUserNameAsync(It.IsAny<string>()))
                       .ReturnsAsync(() => null);

        var act = () => _sut.GetEmployeeProfileAsync(userName);

        await act.Should()
                 .ThrowAsync<NotFoundException>();
    }

    [Theory, AutoData]
    public async Task GetCurrentProfile_ShouldReturnValidData(string userName)
    {
        // Arrange
        var employee = _fixture.Create<Employee>();

        _currentUserServiceMock.Setup(x => x.GetUserName())
                               .Returns(userName);

        _unitOfWorkMock.Setup(x => x.EmployeeRepository.GetByUserNameAsync(It.IsAny<string>()))
                       .ReturnsAsync(employee);

        var expected = _mapper.Map<EmployeeDto>(employee);

        // Act
        var actual = await _sut.GetCurrentProfileAsync();

        // Assert
        actual.Should()
              .BeEquivalentTo(expected);
    }
}