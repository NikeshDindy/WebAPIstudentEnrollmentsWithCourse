using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



// Add this using directive to fix CS1061
using WebAPIstudentEnrollmentsWithCourse.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(); // Adding identity services

// activating Identity APIs
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<AppDbContextForIdentity>();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContextForIdentity>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbCon")));


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapIdentityApi<IdentityUser>(); // mapping identity user

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
