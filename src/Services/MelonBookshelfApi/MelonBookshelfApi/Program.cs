using MelonBookchelfApi.Infrastructure.Data;
using MelonBookchelfApi.Infrastructure.Repositories;
using MelonBookshelfApi.ProgramExtentions;
using MelonBookshelfApi.Services;
using MelonBookshelfApi.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using IResourceService = MelonBookshelfApi.Services.Contracts.IResourceService;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DockerConnection");
builder.Services.AddDbContext<BookshelfDbContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<BookshelfDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddJwtAuthentication(builder.Configuration);
//builder.Services.AddScoped<IResourceService, ResourceService>();
//builder.Services.AddScoped<IRepository, Repository>();
//builder.Services.AddScoped<IRequestService, RequestService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    using var scope = app.Services.CreateScope();

    app.Logger.LogInformation("Starting web host ({ApplicationName})...", appName);
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Host terminated unexpectedly ({ApplicationName})...", appName);

    throw;
}

[ExcludeFromCodeCoverage]
public partial class Program
{
    public const string appName = "MelonBookshelfApi";
}
