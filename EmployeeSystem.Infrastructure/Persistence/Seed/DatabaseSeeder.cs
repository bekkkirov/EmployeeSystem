using EmployeeSystem.Application.Interfaces.Persistence;
using EmployeeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TaskStatus = EmployeeSystem.Domain.Entities.TaskStatus;

namespace EmployeeSystem.Infrastructure.Persistence.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedDatabase(SystemContext context, IUnitOfWork unitOfWork)
    {
        if (!await context.Employees.AnyAsync())
        {
            var employees = new List<Employee>()
            {
                new Manager()
                {
                    FirstName = "Vladyslav",
                    LastName = "Bekirov",
                    UserName = "bekirov",
                    Email = "bekirov@mail.com",
                    BirthDate = new DateTime(2003, 11, 6),
                    Post = Post.SeniorWorker,
                    Salary = 2000
                },

                new Employee()
                {
                    FirstName = "Valdemar",
                    LastName = "Katsmanov",
                    UserName = "v2705",
                    Email = "v2705@mail.com",
                    BirthDate = new DateTime(2002, 5, 20),
                    Post = Post.JuniorWorker,
                    Salary = 800
                },

                new Employee()
                {
                    FirstName = "Eugene",
                    LastName = "Aoevich",
                    UserName = "eugene",
                    Email = "eugene@mail.com",
                    BirthDate = new DateTime(2022, 4, 10),
                    Post = Post.JuniorWorker,
                    Salary = 900
                },
            };

            foreach (var employee in employees)
            {
                unitOfWork.EmployeeRepository.Add(employee);
            }
        }

        if (!await context.Tasks.AnyAsync())
        {
            var tasks = new List<EmployeeTask>()
            {
                new EmployeeTask()
                {
                    Description = "Do something with your life.",
                    Created = new DateTime(2022, 5, 10),
                    CompleteBy = new DateTime(2022, 5, 20),
                    Status = TaskStatus.Active,
                    EmployeeId = 2,
                    ManagerId = 1,
                },

                new EmployeeTask()
                {
                    Description = "Write unit tests.",
                    Created = new DateTime(2022, 3, 5),
                    CompleteBy = new DateTime(2022, 4, 5),
                    Status = TaskStatus.SentForReworking,
                    EmployeeId = 3,
                    ManagerId = 1,
                },

                new EmployeeTask()
                {
                    Description = "Sell me this pen.",
                    Created = new DateTime(2022, 6, 10),
                    CompleteBy = new DateTime(2022, 6, 12),
                    Status = TaskStatus.Completed,
                    EmployeeId = 2,
                    ManagerId = 1,
                },

                new EmployeeTask()
                {
                    Description = "Check your profile.",
                    Created = new DateTime(2022, 1, 5),
                    CompleteBy = new DateTime(2022, 1, 6),
                    Status = TaskStatus.Approved,
                    EmployeeId = 3,
                    ManagerId = 1,
                },
            };

            foreach (var task in tasks)
            {
                unitOfWork.TaskRepository.Add(task);
            }
        }

        await unitOfWork.SaveChangesAsync();
    }
}