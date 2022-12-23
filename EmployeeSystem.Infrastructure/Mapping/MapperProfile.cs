using AutoMapper;
using EmployeeSystem.Application.Models;
using EmployeeSystem.Domain.Entities;

namespace EmployeeSystem.Infrastructure.Mapping;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeTask, TaskDto>();
        CreateMap<SignUpDto, Employee>();
    }
}