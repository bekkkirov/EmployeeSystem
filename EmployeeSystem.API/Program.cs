using EmployeeSystem.API.Extensions;

namespace EmployeeSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbConnections(builder.Configuration);
            builder.Services.AddUnitOfWork();
            builder.Services.AddAutoMapper();
            builder.Services.AddApplicationServices();
            builder.Services.AddJwtAuthentication(builder.Configuration);

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}