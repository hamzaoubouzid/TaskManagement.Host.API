using Microsoft.EntityFrameworkCore;
using TaskManagement.Business;
using TaskManagement.Notifier;
using TaskManagement.Scheduler;
using TaskManagement.Services.Contract;
using TaskManagement.Services.EF;
using TaskManagement.Services.EF.Context;

var builder = WebApplication.CreateBuilder(args);
 



// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<TaskManagmentAppContext>(options => options.UseSqlServer(
     builder.Configuration.GetConnectionString("MyDataBaseSqlServer")
));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// services  Contrat
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();
builder.Services.AddScoped<ITeachersRepository, TeachersRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskScheduler, TasksScheduler>();
builder.Services.AddScoped<ITaskManagmentNotifier,TaskManagementNotifier>();
// end services  Contrat
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
