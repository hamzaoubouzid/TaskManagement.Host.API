using Microsoft.EntityFrameworkCore;
using TaskManagement.Business;
using TaskManagement.Notifier;
using TaskManagement.Scheduler;
using TaskManagement.Services.Contract;
using TaskManagement.Services.EF.Context;
using TaskManagement.Services.Mongo;

var builder = WebApplication.CreateBuilder(args);
 



// Add services to the container.

builder.Services.AddControllers();


// MongoDb Connection String
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MyDataBaseMongoDb"));

// SqlSever Connection String
builder.Services.AddDbContext<TaskManagmentAppContext>(options => options.UseSqlServer(
     builder.Configuration.GetConnectionString("MyDataBaseSqlServer")
));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// services  Contrat Sql
builder.Services.AddScoped<IStudentsRepository, TaskManagement.Services.EF.StudentsRepository>();
builder.Services.AddScoped<ITeachersRepository, TaskManagement.Services.EF.TeachersRepository>();
builder.Services.AddScoped<ITaskRepository, TaskManagement.Services.EF.TaskRepository>();
// services  Contrat MongoDb
builder.Services.AddScoped<IStudentsRepository, TaskManagement.Services.Mongo.StudentsRepository>();
builder.Services.AddScoped<ITeachersRepository, TaskManagement.Services.Mongo.TeachersRepository>();
builder.Services.AddScoped<ITaskRepository, TaskManagement.Services.Mongo.TaskRepository>();
builder.Services.AddScoped<ITaskScheduler, TasksScheduler>();
builder.Services.AddScoped<ITaskManagmentNotifier,TaskManagementNotifier>();
// Add   memory cache dependencies 
builder.Services.AddDistributedMemoryCache(); 
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
