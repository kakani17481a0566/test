using Microsoft.EntityFrameworkCore;
using test.Data;
using test.Services.Lead;
using test.Services.Roles;
using test.Services.User;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure services

// Configure PostgreSQL database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Register application services
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<LeadService>();
builder.Services.AddScoped<LeadServiceSummary>();

// Add CORS - allow all origins (use only for development!)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 2. Configure HTTP request pipeline

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS before authorization
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
