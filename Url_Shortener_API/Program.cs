using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Url_Shortener_API.Data;
using Url_Shortener_API.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Mongo 
        builder.Services.Configure<MongoDatabaseSettings>(
            builder.Configuration.GetSection("MongoDB"));

        // Service Layer Dependency Injection
        builder.Services.AddServices();

        // Fluent validation
        builder.Services.AddFluentValidation(fluentValidation => fluentValidation.RegisterValidatorsFromAssemblyContaining<Program>(lifetime: ServiceLifetime.Singleton));

        //CORS
        builder.Services.AddCors(policyBuilder =>
            policyBuilder.AddDefaultPolicy(policy =>
                policy.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
            )
        );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        
        app.UseCors();
        
        app.UseAuthentication();
        
        app.UseAuthorization();
        
        app.MapControllers();

        app.MapGet("/api/alive", () => new OkResult());

        app.Run();
    }
}