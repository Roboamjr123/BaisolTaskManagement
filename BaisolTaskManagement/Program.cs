using AuthLibrary.Services.Interface;
using AuthLibrary.Services.Repositories;
using BaisolTaskManagement.Helper;
using DataLibrary.Database;
using DataLibrary.DataFormat;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// AutoMapper setup
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddScoped<IProjects, ProjectsRepository>();
builder.Services.AddScoped<ITasks, TasksRepository>();
builder.Services.AddScoped<ISubTasks, SubTasksRepository>();
builder.Services.AddScoped<ISubDetails, SubDetailsRepository>();

// Add controllers and custom JSON converters
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateConvert());
    });

// Configure DbContext with PostgreSQL
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"),
        x => x.MigrationsAssembly("DataLibrary")); // Ensure 'DataLibrary' is correct
});

// Swagger/OpenAPI setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(/*c =>
{
    c.SchemaFilter<DateFormatSchemaFilter(); // Add your custom schema filter
}*/);

// CORS Setup
builder.Services.AddCors(options =>
{
    var frontEndUrl = builder.Configuration.GetSection("FrontEnd_Url").Get<string[]>();
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(frontEndUrl)
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Use CORS in the pipeline
app.UseCors();

app.MapControllers();

app.Run();
