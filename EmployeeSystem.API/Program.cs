using EmployeeSystem.API.Extensions;
using EmployeeSystem.Application.Interfaces.Persistence;
using EmployeeSystem.Infrastructure.Persistence;
using EmployeeSystem.Infrastructure.Persistence.Seed;

namespace EmployeeSystem.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbConnections(builder.Configuration);
            builder.Services.AddUnitOfWork();
            builder.Services.AddAutoMapper();
            builder.Services.AddApplicationOptions(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.Services.AddIdentity();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                await DatabaseSeeder.SeedDatabase(scope.ServiceProvider.GetRequiredService<SystemContext>(),
                    scope.ServiceProvider.GetRequiredService<IUnitOfWork>());
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}