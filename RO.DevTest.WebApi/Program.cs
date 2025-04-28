using RO.DevTest.Infrastructure.IoC;
using RO.DevTest.Persistence.IoC;
using System.Text.Json.Serialization;
using BE.Application;

namespace RO.DevTest.WebApi;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.InjectPersistenceDependencies()
            .InjectInfrastructureDependencies(builder.Configuration);

        // Add Mediatr to program
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                typeof(ApplicationLayer).Assembly,
                typeof(Program).Assembly
            );
        });

        builder.Services.AddCors(op =>
        {
            op.AddPolicy("LocalPolyce", policy =>
            {
                policy.WithOrigins("http://localhost:*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                policy.WithOrigins("https://localhost:*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if(app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

		app.UseHttpsRedirection();

		app.UseCors("LocalPolyce");
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}
